using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CustomPlayerSpawn : NetworkBehaviour
{
	//private static int m_nextId = 0;    //TODO: May need a way to reset this.
	//private int m_playerId;

	public override void OnStartLocalPlayer() // this is our player
	{
		base.OnStartLocalPlayer();

		//	m_playerId = m_nextId++; //alternative: this.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId

		//	//GameObject playerObj = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerControllerId]); // instantiate on server side
		//	//      NetworkServer.AddPlayerForConnection(NetworkServer.connections[playerControllerId], playerObj, playerControllerId); // spawn on the clients and set owner

		if (isLocalPlayer)
		{
			//setAuthority(this.GetComponent<NetworkIdentity>());
			CmdSetAuthority(this.GetComponent<NetworkIdentity>()); //OLD: (m_playerId, this.GetComponent<NetworkIdentity>())
																   //GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
																   //if (networkAuthorityGroup)
																   //{
																   //	AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
																   //	if (assignPlayerAuthority)
																   //	{
																   //		assignPlayerAuthority.assignAuthority(playerControllerId, connectionToClient);
																   //	}
																   //}
		}

		//if (isLocalPlayer)
		//{
		//OnLevelWasLoaded();
		//DontDestroyOnLoad(this);
		//}
	}

	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	// Reference: http://answers.unity3d.com/questions/1174255/since-onlevelwasloaded-is-deprecated-in-540b15-wha.html
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (isLocalPlayer)
		{
			CmdSetAuthority(this.GetComponent<NetworkIdentity>());
			//setAuthority(this.GetComponent<NetworkIdentity>());
		}
	}

	void OnLevelWasLoaded()
	{
	//	if (isLocalPlayer)
	//	{
	//		CmdSetAuthority(this.GetComponent<NetworkIdentity>());
		}
//		if(isServer)
//		{
//			GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
//			if (networkAuthorityGroup)
//			{
//				AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
//				if (assignPlayerAuthority)
//				{
//					assignPlayerAuthority.
//                    assignPlayerAuthority.assignAuthority(playerNetID.clientAuthorityOwner.connectionId, playerNetID.clientAuthorityOwner);//alt: (playerId, playerNetID.connectionToClient);
//				}
//			}
//		}

//		if (isLocalPlayer)
//		{
////			CmdSetAuthority(this.GetComponent<NetworkIdentity>());
//			//			DontDestroyOnLoad(this);
//		}
	//}

	[Command]
	void CmdSetAuthority(NetworkIdentity playerNetID)
	{
		//DontDestroyOnLoad(this);
		RpcSetAuthority(playerNetID);
		//setAuthority(playerNetID);
	}

	[ClientRpc]
	void RpcSetAuthority(NetworkIdentity playerNetID)
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
				assignPlayerAuthority.assignAuthority(playerNetID.clientAuthorityOwner.connectionId, playerNetID.clientAuthorityOwner);//alt: (playerId, playerNetID.connectionToClient);
			}
		}
	}

	//void Start()
	//{

	//}

	public override void OnStartClient()
	{
		base.OnStartClient();
		DontDestroyOnLoad(this);
		//	//NetworkManager.Instantiate(NetworkManager.singleton.spawnPrefabs[0]);
	}
}
