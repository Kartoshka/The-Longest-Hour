using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetPlayerAuthority : MonoBehaviour
{
	void OnLevelWasLoaded()
	{
		//if(Network.isServer)
		//{
		//	RpcSetAuthority();
		//}
	//	ClientScene.AddPlayer(0);
	}

	//[ClientRpc] // Make the change.
	//private void RpcSetAuthority()
	//{
	//	GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
	//	if (networkAuthorityGroup)
	//	{
	//		AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
	//		if (assignPlayerAuthority)
	//		{
	//			foreach (NetworkConnection netConn in NetworkServer.connections)
	//			{
	//				assignPlayerAuthority.assignAuthority(netConn.connectionId, netConn);
	//			}
	//			//for (int i = 0; i < lobbySlots.Length; ++i)
	//			//{
	//			//	NetworkLobbyPlayer player = lobbySlots[i];
	//			//	if(player.connectionToClient != null)
	//			//	{
	//			//		assignPlayerAuthority.assignAuthority(player.slot, player.connectionToClient);
	//			//	}
	//			//}
	//		}
	//	}
	//}
}
