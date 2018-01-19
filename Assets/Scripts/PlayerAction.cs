using System.Collections;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.IO;
using System.Drawing;
using Emgu.CV.Util;
using UnityEngine;
using System;

public class PlayerAction : MonoBehaviour {

    private VideoCapture webcam;
    private Mat webcamFrame;
    private Mat webcamFrameGray;

    private CascadeClassifier cascadeClassifier;
    private string path = "D:\\Gamagora\\Interface\\ProjectOpenCV2D\\data\\lbpcascades\\lbpcascade_frontalface_improved.xml";

    private Rectangle[] faces;

    private PlayerController playerController;

    private void Start() {
        webcam = new VideoCapture(0);
        webcam.ImageGrabbed += new EventHandler(handleWebcamQueryFrame);
        webcamFrame = new Mat();
        webcamFrameGray = new Mat();
        cascadeClassifier = new CascadeClassifier(path);

        playerController = GetComponent<PlayerController>();
    }

    private void Update() {
        if (webcam.IsOpened) {
            webcam.Grab();
        }
        if (webcamFrame.IsEmpty) {
            return;
        }
    }

    private void OnDestroy() {
        webcam.Dispose();
        CvInvoke.DestroyAllWindows();
    }

    private void handleWebcamQueryFrame(object sender, EventArgs e) {
        webcam.Retrieve(webcamFrame);

        Image<Hsv, byte> imageHSV = webcamFrame.ToImage<Hsv, byte>();
        CvInvoke.CvtColor(imageHSV, imageHSV, ColorConversion.Bgr2Hsv);

        double hValueMin = 70;
        double hValueMax = 100;

        double sValueMin = 100;
        double sValueMax = 255;

        double vValueMin = 100;
        double vValueMax = 255;

        Hsv lower = new Hsv(hValueMin, sValueMin, vValueMin);
        Hsv upper = new Hsv(hValueMax, sValueMax, vValueMax);

        Mat imgGray = imageHSV.InRange(lower, upper).Mat;

        VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
        VectorOfPoint biggestContour = new VectorOfPoint();
        int biggestContourIndex = -1;
        float biggestContourArea = 0f;

        Mat hierarchy = new Mat();
        CvInvoke.FindContours(imgGray, contours, hierarchy, RetrType.List, ChainApproxMethod.ChainApproxNone);

        for (int i = 0; i < contours.Size; i++) {
            if (CvInvoke.ContourArea(contours[i]) > biggestContourArea) {
                biggestContour = contours[i];
                biggestContourIndex = i;
                biggestContourArea = (float)CvInvoke.ContourArea(contours[i]);
            }
        }

        if (biggestContourIndex > -1) {
            CvInvoke.DrawContours(webcamFrame, contours, biggestContourIndex, new MCvScalar(255, 0, 0));
        }

        var moments = CvInvoke.Moments(biggestContour);
        int cx = (int)(moments.M10 / moments.M00);
        int cy = (int)(moments.M01 / moments.M00);
        Point p = new Point(cx, cy);
        int width = webcamFrame.Width;
        int height = webcamFrame.Height;

        if (cy < (float)height / 2) {
            playerController.setJump();
        }

        Mat flippedImage = webcamFrame.Clone();
        CvInvoke.Flip(webcamFrame, flippedImage, FlipType.Horizontal);
        CvInvoke.Imshow("Test", flippedImage);
    }
}
