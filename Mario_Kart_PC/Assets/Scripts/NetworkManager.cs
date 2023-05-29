using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
	public GameObject playerPrefab;
	public Transform spawn;
	public Camera camara;
	// Start is called before the first frame update
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
		camara = Camera.main;
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to Master");
		PhotonNetwork.JoinRandomOrCreateRoom();
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined a room.");
		PhotonNetwork.Instantiate(playerPrefab.name, spawn.position, spawn.rotation, 0);
	}
}