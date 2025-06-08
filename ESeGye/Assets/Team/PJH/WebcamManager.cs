using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamManager : MonoBehaviour
{
    public RawImage webcamDisplay;
    private WebCamTexture webcam;

    void Start()
    {
        if (webcamDisplay == null)
        {
            Debug.LogWarning("WebcamManager: RawImage not assigned.");
            return;
        }

        webcam = new WebCamTexture();
        webcamDisplay.texture = webcam;
        webcam.Play();
    }
}

