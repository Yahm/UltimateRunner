using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.IO;
using System.Drawing;
using Emgu.CV.Util;
using UnitySampleAssets.CrossPlatformInput;
using System;

namespace UnitySampleAssets._2D
{
    public class EmguScript : MonoBehaviour
    {

        private PlayerController playerController;

        VideoCapture webcam;
        VideoWriter writer;
        Mat matimage;
        Mat hierarchy = new Mat();
        Mat mat, mat_original;
        bool rawImg = false;

        MCvMoments moments;
        Image<Hsv, byte> imageHSV;

        private PlatformerCharacter2D character;

        private bool jump = false;


        public GameObject go;

        MCvScalar mcv_color = new MCvScalar(255, 0, 0);

        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        VectorOfPoint biggestContour = new VectorOfPoint();
        int biggestContourIndex;
        double biggestContourArea = 0;
        double biggestContourArea_past = 0;
        double biggestAreaAllOf = 0;
        double seuilArea = 5000;

        Mat matGray, matHSV;
        Hsv hsvbas, hsvhaut;

        int size = 15;
        int width, height;

        Texture2D matTexture;
        public UnityEngine.UI.RawImage rawImage;

        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();

            playerController = GetComponent<PlayerController>();

           // rawImage = 

        }

        // Use this for initialization
        void Start()
        {
            mat = new Mat();
            matGray = new Mat();
            matGray = new Mat();
            matHSV = new Mat();

            

            //valeur de seuil
            double HvalueBas = 70; //green
            double HvalueHaut = 100;

            double SvalueBas = 100;
            double SvalueHaut = 255;

            double VvalueBas = 100;
            double VvalueHaut = 255;

            hsvbas = new Hsv(HvalueBas, SvalueBas, VvalueBas);
            hsvhaut = new Hsv(HvalueHaut, SvalueHaut, VvalueHaut);

            webcam = new VideoCapture(0);
            writer = new VideoWriter("hello.mp4", 24, new Size(300, 300), true);
            matTexture = new Texture2D(webcam.Width, webcam.Height);
            
            webcam.ImageGrabbed += new System.EventHandler(handleImg);
            //webcam.Start();

        }

        // Update is called once per frame
        void Update()
        {

            if (webcam.IsOpened)
            {
                webcam.Grab();

            }
        }


        //fonction de traitement d'image
        private void handleImg(object sender, System.EventArgs e)
        {
            if (webcam.IsOpened)
            {
                webcam.Retrieve(mat);

                if (mat.IsEmpty)
                {
                    return;
                }

                CvInvoke.Flip(mat, mat, FlipType.Horizontal);

                width = mat.Width;
                height = mat.Height;


                //conversion de l'image en hsv
                //hsv
                CvInvoke.CvtColor(mat, mat, ColorConversion.Bgr2Hsv);

                imageHSV = mat.ToImage<Hsv, byte>();

                


                //segmentation
                matGray = imageHSV.InRange(hsvbas, hsvhaut).Mat;

                //element structurant
                int operationSize = 1;
                Mat elem = CvInvoke.GetStructuringElement(ElementShape.Cross,
                                                       new Size(2 * operationSize + 1, 2 * operationSize + 1),
                                                       new Point(operationSize, operationSize));

                //ouverture
                CvInvoke.Erode(matGray, matGray, elem, new Point(1, 1), 1, BorderType.Constant, CvInvoke.MorphologyDefaultBorderValue);
                CvInvoke.Dilate(matGray, matGray, elem, new Point(1, 1), 1, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);


                //Detection des contours
                CvInvoke.FindContours(matGray, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxNone);

                if (contours.Size > 0)
                {
                    biggestContourArea = CvInvoke.ContourArea(contours[0]);
                    for (int i = 0; i < contours.Size; i++)
                    {

                        if (CvInvoke.ContourArea(contours[i]) >= biggestContourArea)
                        {
                            biggestContour = contours[i];
                            biggestContourIndex = i;
                            biggestContourArea = CvInvoke.ContourArea(contours[i]);
                        }
                    }

                    if (biggestContourArea > 0)
                    {
                        //centroid : 

                        /*moments = CvInvoke.Moments(biggestContour);
                        int cx = (int)(moments.M10 / moments.M00);
                        int cy = (int)(moments.M01 / moments.M00);
                        Point centroid = new Point(cx, cy);*/
                        //Debug.Log(biggestContourArea);
                        if (biggestContourArea_past < biggestContourArea)
                        {
                            // Action de saut si l'aire du contour atteint un certain seuil
                            if (biggestContourArea >= seuilArea)
                            {
                             

                                //if (!jump)
                                //{
                                    // Read the jump input in Update so button presses aren't missed.
                                    //jump = true;
                                    playerController.setJump();
                                //    jump = false;
                                //}

                                //go.transform.Translate(Vector3.right);
                            }
                        }

                        biggestContourArea_past = biggestContourArea;
                        
                        CvInvoke.DrawContours(mat, contours, biggestContourIndex, mcv_color, 10);
                    }
                }


                CvInvoke.CvtColor(mat, mat, ColorConversion.Hsv2Bgr);
                //CvInvoke.Imshow("Window", mat); // affichage externe
                rawImage.texture = convertFromMatToTexture2D(mat, matTexture); // affichage fenêtre dans unity
                rawImg = true;

            }
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            //character.Move(0, false, jump);
            

            /*if(jump)
            {
                playerController.setJump();
                jump = false;
            }
            if (rawImg)
            {
                rawImage.texture = convertFromMatToTexture2D(mat, matTexture); // affichage fenêtre dans unity
                rawImg = false;
            }*/
            
        }

        void OnDestroy()
        {
            CvInvoke.DestroyAllWindows();
            webcam.Stop();
        }


        private Texture2D convertFromMatToTexture2D(Mat matImage, Texture2D tex)
        {
            MemoryStream memstream = new MemoryStream();
            matImage.ToImage<Bgr, byte>().ToBitmap().Save(stream: memstream,
                                                        format: matImage.ToImage<Bgr, byte>().ToBitmap().RawFormat);
            tex.LoadImage(memstream.ToArray());
            return tex;
        }
    }
}
