using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CustomNetworkedPlayer : NetworkBehaviour
{
	public override void OnStartClient()
	{
		base.OnStartClient();
		DontDestroyOnLoad(this);
	}

	public override void OnStartLocalPlayer() // this is our player
	{
		base.OnStartLocalPlayer();

		//CmdInstantiateCustomPlayer(this.GetComponent<NetworkIdentity>());
		//instantiateCustomPlayer(this.GetComponent<NetworkIdentity>());
		//NetworkIdentity netId = this.GetComponent<NetworkIdentity>();
		//if(netId)
		//{
		//	if(netId.connectionToServer != null)
		//	{
		//		NetworkServer.SpawnWithClientAuthority(NetworkLobbyManager.singleton.spawnPrefabs[netId.connectionToServer.connectionId], this.gameObject);
		//	}
		//}

//		tryInstantiatePlayerObject();
  //      NetworkIdentity netId = this.GetComponent<NetworkIdentity>();
		//if (netId)
		//{
		//	NetworkConnection netConn = null;
		//	if (netId.clientAuthorityOwner != null)
		//	{
		//		netConn = netId.clientAuthorityOwner;
		//	}
		//	else
		//	{
		//		netConn = netId.connectionToServer;
		//	}
		//	if (netConn != null)
		//	{
		//		//Transform spawnTranform = NetworkLobbyManager.singleton.startPositions[netConn.connectionId];
		//		//GameObject spawnedObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[netConn.connectionId], spawnTranform.position, spawnTranform.rotation, this.transform);

		//		if (isClient)
		//		{
		//			CmdInstantiateCustomPlayer(netId, null);
		//		}
		//		else if (isServer)
		//		{
		//			RpcInstantiateCustomPlayer(netId, null);
		//		}

		//		//CmdInstantiateCustomPlayer(netId, spawnedObject);
		//	}
		//}

		//if (isServer)
		//{
		//	//	CmdSetAuthority(this.GetComponent<NetworkIdentity>());
		//	NetworkIdentity netId = this.GetComponent<NetworkIdentity>();
		//	if (netId)
		//	{
		//		if (netId.connectionToServer != null)
		//		{
		//			NetworkServer.SpawnWithClientAuthority(NetworkLobbyManager.singleton.spawnPrefabs[netId.connectionToServer.connectionId], this.gameObject);
		//		}
		//	}
		//}
	}

	void OnEnable()
	{
//		tryInstantiatePlayerObject();

		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled.
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	// Reference: http://answers.unity3d.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
//		tryInstantiatePlayerObject();
		//if (isServer)
		//{
		//	NetworkIdentity netId = this.GetComponent<NetworkIdentity>();
		//	if (netId)
		//	{
		//		if (netId.connectionToServer != null)
		//		{
		//			NetworkServer.SpawnWithClientAuthority(NetworkLobbyManager.singleton.spawnPrefabs[netId.connectionToServer.connectionId], this.gameObject);
		//		}
		//	}

		//	//	CmdSetAuthority(this.GetComponent<NetworkIdentity>());
		//	//instantiateCustomPlayer(this.GetComponent<NetworkIdentity>());
		//}
	}

	private void tryInstantiatePlayerObject()
	{
		NetworkIdentity netId = this.GetComponent<NetworkIdentity>();
		if (netId)
		{
			NetworkConnection netConn = null;
			if (netId.clientAuthorityOwner != null)
			{
				netConn = netId.clientAuthorityOwner;
			}
			else
			{
				netConn = netId.connectionToServer;
			}
			if (netConn != null)
			{
				if (isClient)
				{
					CmdInstantiateCustomPlayer(netId, null);
				}
				else if (isServer)
				{
					RpcInstantiateCustomPlayer(netId, null);
				}
			}
		}
	}

	bool waitingForDelayedStart = true;
	float timer = 1;
	void FixedUpdate()
	{
		if(waitingForDelayedStart)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				waitingForDelayedStart = false;
				tryInstantiatePlayerObject();
            }
		}
	}

	[Command]
	public void CmdInstantiateCustomPlayer(NetworkIdentity playerNetID, GameObject spawnedObject)
	{
		//RpcInstantiateCustomPlayer(playerNetID, spawnedObject);
		instantiateCustomPlayer(playerNetID, spawnedObject);
	}

	[ClientRpc]
	public void RpcInstantiateCustomPlayer(NetworkIdentity playerNetID, GameObject spawnedObject)
	{
		instantiateCustomPlayer(playerNetID, spawnedObject);
	}

	private void instantiateCustomPlayer(NetworkIdentity playerNetID, GameObject spawnedObject)
	{
		//Transform spawnTranform = NetworkLobbyManager.singleton.startPositions[playerNetID.clientAuthorityOwner.connectionId];

		//GameObject playerObject = (GameObject)Network.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerNetID.clientAuthorityOwner.connectionId], spawnPosition, Quaternion.identity, 0);
		//GameObject spawnedObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerNetID.clientAuthorityOwner.connectionId], spawnTranform.position, spawnTranform.rotation, this.transform);
		//if (spawnedObject)
		//{
			NetworkConnection netConn = null;
			if (playerNetID.clientAuthorityOwner != null)
			{
				netConn = playerNetID.clientAuthorityOwner;
			}
			else
			{
				netConn = playerNetID.connectionToServer;
			}
			if(netConn != null)
			{
				Transform spawnTranform = NetworkLobbyManager.singleton.startPositions[netConn.connectionId];
				GameObject playerObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[netConn.connectionId], spawnTranform.position, spawnTranform.rotation, this.transform);
				NetworkServer.SpawnWithClientAuthority(playerObject, netConn);
			}
			//NetworkServer.SpawnWithClientAuthority(customPlayerObject, playerNetID.clientAuthorityOwner); //.AddPlayerForConnection(playerNetID.clientAuthorityOwner, playerObject, playerNetID.playerControllerId);
			//NetworkServer.Spawn(spawnedObject);
        //}
	}

	//[Command]
	//public void CmdInstantiateCustomPlayer(NetworkIdentity playerNetID)
	//{
	//	instantiateCustomPlayer(playerNetID);
 //   }

	//private void instantiateCustomPlayer(NetworkIdentity playerNetID)
	//{
	//	GameObject spawnPointManagerObject = GameObject.Find("SpawnPointManager");
	//	Vector3 spawnPosition = this.transform.position; ;
	//	if(spawnPointManagerObject)
	//	{
	//		SpawnPointManager spawnPointManager = spawnPointManagerObject.GetComponent<SpawnPointManager>();
	//		if(spawnPointManager)
	//		{
	//			spawnPosition = spawnPointManager.getPlayerSpawnPosition(playerNetID.clientAuthorityOwner.connectionId);
 //           }
	//	}
		
	//	//GameObject playerObject = (GameObject)Network.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerNetID.clientAuthorityOwner.connectionId], spawnPosition, Quaternion.identity, 0);
	//	GameObject playerObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerNetID.clientAuthorityOwner.connectionId], spawnPosition, Quaternion.identity, this.transform);
	//	if (playerObject)
	//	{
	//		NetworkServer.AddPlayerForConnection(playerNetID.clientAuthorityOwner, playerObject, playerNetID.playerControllerId);
	//	}
	//}

	[Command]
	public void CmdSetAuthority(NetworkIdentity playerNetID)
	{
		setAuthority(playerNetID);
	}

	private void setAuthority(NetworkIdentity playerNetID)
	{
		GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
		if (networkAuthorityGroup)
		{
			AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
			if (assignPlayerAuthority)
			{
				assignPlayerAuthority.assignAuthority(playerNetID.clientAuthorityOwner.connectionId, playerNetID.clientAuthorityOwner);
			}
		}
	}
}
