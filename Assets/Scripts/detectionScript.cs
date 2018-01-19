using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.IO;

public class detectionScript : MonoBehaviour {

    VideoCapture webcam;
    bool isImg;
    Mat mat,matGray;

    Rectangle[] frontFaces;
    int min_face_size = 50;
    int max_face_size = 200;

    CascadeClassifier frontfaceCascadeClassifier;

    string absolutePathToFrontFaceCascadeClassifier = "haarcascade_frontalface_default.xml";

    Texture2D matTexture;
    public UnityEngine.UI.RawImage rawImage;

	// Use this for initialization
	void Start () {
        webcam = new VideoCapture(0);
        mat = new Mat();
        matGray = new Mat();

        webcam.ImageGrabbed += new System.EventHandler(handleImg);

        frontfaceCascadeClassifier = new CascadeClassifier(fileName: "haarcascades/" + absolutePathToFrontFaceCascadeClassifier);

        matTexture = new Texture2D(webcam.Width,webcam.Height);
	}
	
	// Update is called once per frame
	void Update () {
        if(webcam.IsOpened)
        {
            webcam.Grab();
        }
	}

    private void handleImg(object sender,System.EventArgs e)
    {
        if (webcam.IsOpened)
        {
            webcam.Retrieve(mat);

            if (mat.IsEmpty)
            {
                return;
            }

            CvInvoke.Flip(mat, mat, FlipType.Horizontal);

            //conversion en niveau de gris
            CvInvoke.CvtColor(mat, matGray, ColorConversion.Bgr2Gray);

			//detection du visage.
            frontFaces = frontfaceCascadeClassifier.DetectMultiScale(image: matGray,
                                                                     scaleFactor: 1.1,
                                                                     minNeighbors: 5,
                                                                     minSize: new Size(min_face_size, min_face_size),
                                                                     maxSize: new Size(max_face_size, max_face_size));

            /*for(int i = 0;i < frontFaces.Length;i++)
            {
                Rectangle face = frontFaces[i];
                CvInvoke.Rectangle(mat, face, new MCvScalar(200, 100, 0), 5);
            }*/
           

            if(frontFaces.Length > 0)
            {
				//affichage du carré detecté
                CvInvoke.Rectangle(mat, frontFaces[0], new MCvScalar(200, 100, 0), 5);
            }
            //CvInvoke.Imshow("Window", mat);

            rawImage.texture = convertFromMatToTexture2D(mat, matTexture);
        }
    }

    private Texture2D convertFromMatToTexture2D(Mat matImage,Texture2D tex)
    {
        MemoryStream memstream = new MemoryStream();
        matImage.ToImage<Bgr, byte>().ToBitmap().Save(stream: memstream,
                                                    format: matImage.ToImage<Bgr, byte>().ToBitmap().RawFormat);
        tex.LoadImage(memstream.ToArray());
        return tex; ;
    }
}
