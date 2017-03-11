using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomPlayerSpawn : NetworkBehaviour
{
	private static int m_nextId = 0;    //TODO: May need a way to reset this.
	private int m_playerId;

	public override void OnStartLocalPlayer() // this is our player
	{
		base.OnStartLocalPlayer();

		//	m_playerId = m_nextId++; //alternative: this.GetComponent<NetworkIdentity>().clientAuthorityOwner.connectionId

		//	//GameObject playerObj = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerControllerId]); // instantiate on server side
		//	//      NetworkServer.AddPlayerForConnection(NetworkServer.connections[playerControllerId], playerObj, playerControllerId); // spawn on the clients and set owner

		//if (isLocalPlayer)
		//{
		//	CmdSetAuthority(this.GetComponent<NetworkIdentity>()); //OLD: (m_playerId, this.GetComponent<NetworkIdentity>())
		//	//GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
		//	//if (networkAuthorityGroup)
		//	//{
		//	//	AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
		//	//	if (assignPlayerAuthority)
		//	//	{
		//	//		assignPlayerAuthority.assignAuthority(playerControllerId, connectionToClient);
		//	//	}
		//	//}
		//}

		//if (isLocalPlayer)
		//{
		OnLevelWasLoaded();
		//DontDestroyOnLoad(this);
		//}
	}

	void OnLevelWasLoaded()
	{
		if (isLocalPlayer)
		{
//			CmdSetAuthority(this.GetComponent<NetworkIdentity>());
			//			DontDestroyOnLoad(this);
		}
	}

	[Command]
	void CmdSetAuthority(NetworkIdentity playerNetID)
	{
		//DontDestroyOnLoad(this);

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
	//	//NetworkManager.Instantiate(NetworkManager.singleton.spawnPrefabs[0]);
	}
}
