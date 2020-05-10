using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour
{
    //public float rotateSpeed;
    public Slider rotateSpeed;
    public Text speed;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The rotate class has started");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation *= Quaternion.AngleAxis (rotateSpeed.value, new Vector3 (-1f, -0.5f, 1f));
        speed.text = "Speed: " + rotateSpeed.value.ToString("F1");
    }

    public void changeSpeed()
    {
        rotateSpeed.minValue = 0.1f;
        rotateSpeed.maxValue = 2.0f;
    }
}
