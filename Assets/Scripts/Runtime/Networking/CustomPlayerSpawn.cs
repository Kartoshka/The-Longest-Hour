using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomPlayerSpawn : NetworkBehaviour
{

	public override void OnStartLocalPlayer() // this is our player
	{
		base.OnStartLocalPlayer();

		GameObject playerObj = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerControllerId]); // instantiate on server side
        NetworkServer.AddPlayerForConnection(NetworkServer.connections[0], playerObj, playerControllerId); // spawn on the clients and set owner
	}
}
