using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public GameObject avatarPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Photon Manager started.");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("informal connection nailed.");
        RoomOptions roomopt = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("Test1", roomopt, new TypedLobby ("Test1", LobbyType.Default));
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.Instantiate(avatarPrefab.name, new Vector3(), Quaternion.identity, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
