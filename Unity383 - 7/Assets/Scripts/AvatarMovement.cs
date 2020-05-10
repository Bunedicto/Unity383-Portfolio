using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvatarMovement : MonoBehaviour
{
    private float move = 0.0f;
    private float turn = 0.0f;

    public float turnSpeed = 100.0f;
    public float moveSpeed = 10.0f;
    //public float speedMultiplier = 0.1f;
    public Slider rotateSpeed;
    public Text speed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Canvas/DirectionalControls/LeftButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { rotateLeft(); });
        GameObject.Find("Canvas/DirectionalControls/LeftButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stop(); });

        GameObject.Find("Canvas/DirectionalControls/RightButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { rotateRight(); });
        GameObject.Find("Canvas/DirectionalControls/RightButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stop(); });

        GameObject.Find("Canvas/DirectionalControls/ForwardButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { goForward(); });
        GameObject.Find("Canvas/DirectionalControls/ForwardButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stop(); });

        GameObject.Find("Canvas/DirectionalControls/BackwardButton").GetComponent<EventTrigger>().triggers[0].callback.AddListener((data) => { goBackward(); });
        GameObject.Find("Canvas/DirectionalControls/BackwardButton").GetComponent<EventTrigger>().triggers[1].callback.AddListener((data) => { stop(); });
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.position += v * transform.forward * Time.deltaTime * 100.0f;
        transform.rotation *= Quaternion.AngleAxis(h * 1000.0f * Time.deltaTime, transform.up);
        */

        transform.rotation *= Quaternion.AngleAxis(turn * turnSpeed * rotateSpeed.value * Time.deltaTime, Vector3.up);
        transform.position += move * moveSpeed * rotateSpeed.value * transform.forward;
        speed.text = (rotateSpeed.value * 10).ToString("F0");
    }

    public void rotateLeft()
    {
        turn = -1.0f;
    }

    public void rotateRight()
    {
        turn = 1.0f;
    }

    public void goForward()
    {
        move = 1.0f;
    }

    public void goBackward()
    {
        move = -1.0f;
    }

    public void stop()
    {
        move = 0.0f;
        turn = 0.0f;
    }

    public void changeSpeed()
    {
        rotateSpeed.minValue = 0.1f;
        rotateSpeed.maxValue = 1.0f;
    }
}
