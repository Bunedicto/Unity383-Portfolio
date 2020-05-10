using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Photon.Pun;

public class CharacterMovement : MonoBehaviour
{
    private float move = 0.0f;
    private float turn = 0.0f;

    public float turnSpeed = 100.0f;
    public float moveSpeed = 10.0f;
    public float speedMultiplier = 0.5f;
    public Text speed;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PhotonView>().IsMine == true || PhotonNetwork.IsConnected == false)
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(turn * turnSpeed * speedMultiplier * Time.deltaTime, Vector3.up);
        transform.position += move * moveSpeed * speedMultiplier * transform.forward;
        speed.text = speedMultiplier.ToString("F1");
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
}
