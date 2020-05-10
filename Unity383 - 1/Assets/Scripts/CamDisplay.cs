using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamDisplay : MonoBehaviour
{
    public Material cameramaterial;
    //public Text message;
    public Text timeElapsed;
    private WebCamTexture webcamTexture;

    // Start is called before the first frame update
    void Start()
    {
        webcamTexture = new WebCamTexture();
        cameramaterial.mainTexture = webcamTexture;
        webcamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed.text = "Time Update: " + Mathf.Round(Time.time);
    }
}
