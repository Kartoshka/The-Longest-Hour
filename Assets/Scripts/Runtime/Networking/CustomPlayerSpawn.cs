using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomPlayerSpawn : NetworkBehaviour
{

	public override void OnStartLocalPlayer() // this is our player
	{
		base.OnStartLocalPlayer();

		//GameObject playerObj = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerControllerId]); // instantiate on server side
  //      NetworkServer.AddPlayerForConnection(NetworkServer.connections[playerControllerId], playerObj, playerControllerId); // spawn on the clients and set owner

		//this.
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		//NetworkManager.Instantiate(NetworkManager.singleton.spawnPrefabs[0]);
	}
}
