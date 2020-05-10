using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeElapsed : MonoBehaviour
{
    public Text timeElapsed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed.text = "Time Elapsed: " + Mathf.Round(Time.time);
    }
}
