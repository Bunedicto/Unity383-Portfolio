using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    public float distance = 3.0f;
    //public float frequency = 0.3f;

    public Slider rotateSpeed;
    public Text speed;

    // Update is called once per frame
    void Update()
    {
        float z = distance * Mathf.Sin(rotateSpeed.value * 2 * Mathf.PI * Time.time);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
        speed.text = rotateSpeed.value.ToString("F1");
    }

    public void changeSpeed()
    {
        rotateSpeed.minValue = 0.1f;
        rotateSpeed.maxValue = 1.0f;
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */
}
