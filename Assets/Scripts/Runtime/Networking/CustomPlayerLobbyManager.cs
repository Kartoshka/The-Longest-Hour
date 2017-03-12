// Reference: https://forum.unity3d.com/threads/how-to-set-individual-playerprefab-form-client-in-the-networkmanger.348337/#post-2256378
// Reference: http://abrgame.blogspot.ca/2016/01/using-unet-to-spawn-different-player.html
// Reference: https://forum.unity3d.com/threads/cant-get-character-selection-working-with-multiplayer.438921/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class CustomPlayerLobbyManager : LobbyManager
{
	
	//public virtual void OnClientConnect(NetworkConnection conn)
	//{
	//	if (string.IsNullOrEmpty(m_OnlineScene) || (m_OnlineScene == m_OfflineScene))
	//	{
	//		ClientScene.Ready(conn);
	//		if (m_AutoCreatePlayer)
	//		{
	//			ClientScene.AddPlayer(0);
	//		}
	//	}
	//	else
	//	{
	//		// player will be added when on-line scene finishes loading
	//	}
	//}

	// Reference: https://forum.unity3d.com/threads/solved-server-not-adding-player-for-reconnecting-client.333171/
	public override void OnClientSceneChanged(NetworkConnection conn)
	{
		base.OnClientSceneChanged(conn);

		// always become ready.
		ClientScene.Ready(conn);

		if (!this.autoCreatePlayer)
		{
			return;
		}

		bool addPlayer = false;
		if (ClientScene.localPlayers.Count == 0)
		{
			// no players exist
			addPlayer = true;
		}

		bool foundPlayer = false;
		foreach (var playerController in ClientScene.localPlayers)
		{
			if (playerController.gameObject != null)
			{
				foundPlayer = true;
				break;
			}
		}
		if (!foundPlayer)
		{
			// there are players, but their game objects have all been deleted
			addPlayer = true;
		}
		if (addPlayer)
		{
			ClientScene.AddPlayer(0);
		}
	}

	public int getPlayerSlot(NetworkConnection netConn)
	{
		foreach (NetworkLobbyPlayer lobbyPlayer in lobbySlots)
		{
			if (lobbyPlayer.connectionToClient == netConn 
				|| lobbyPlayer.connectionToServer == netConn)
			{
				return lobbyPlayer.slot;
			}
        }
		return -1;
	}

	//public List<List<GameObject>> m_localPlayerAuthorityObjects;

	//private int playerPrefabIndex = 0;
	//Dictionary<int, int> currentPlayers;

	//public void addLocalPlayerAuthorityObject(int playerId, GameObject objectToBeControlled)
	//{
	//	m_localPlayerAuthorityObjects[playerId].Add(objectToBeControlled);
	//   }

	//Dictionary<int, int> currentPlayers = new Dictionary<int, int>();
	//public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
	//{
	//	if (!currentPlayers.ContainsKey(conn.connectionId))
	//		currentPlayers.Add(conn.connectionId, playerPrefabIndex++); // TODO: Set this to 0, and use a function to attach the playerPrefabIndex with the connectionID.

	//	return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
	//}

	//public void SetPlayerTypeLobby(NetworkConnection conn, int playerPrefabIndex)
	//{
	//	if (currentPlayers.ContainsKey(conn.connectionId))
	//		currentPlayers[conn.connectionId] = playerPrefabIndex;
	//}

	//public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
	//{
	//	GameObject gamePlayer = base.OnLobbyServerCreateGamePlayer(conn, playerControllerId);
	//	DontDestroyOnLoad(gamePlayer);
	//	return gamePlayer;
	//}

	//public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
	//{
	//	int index = currentPlayers[conn.connectionId];

	//	GameObject _temp = (GameObject)GameObject.Instantiate(spawnPrefabs[index],
	//		startPositions[conn.connectionId].position,//GetStartPosition().position
	//		Quaternion.identity);

	//	//NetworkServer.AddPlayerForConnection(conn, _temp, playerControllerId);

	//	return _temp;
	//}

	//// I have put a toggle UI on gameObjects called PC1 and PC2 to select two different character types.
	//// on toggle, this function is called, which updates the playerPrefabIndex
	//// The index will be the number from the registered spawnable prefabs that 
	//// you want for your player
	//public void To()
	//{
	//	if (GameObject.Find("PC1").GetComponent<Toggle>().isOn)
	//	{
	//		playerPrefabIndex = 0;
	//	}
	//	//else if (GameObject.Find("PC2").GetComponent<Toggle>().isOn)
	//	//{
	//	//	playerPrefabIndex = 1;
	//	//}
	//	else
	//	{
	//		playerPrefabIndex = 1;
	//	}
	//}


	//public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	//{
	//	this.playerPrefab = this.spawnPrefabs[playerControllerId];
	//	base.OnServerAddPlayer(conn, playerControllerId);
	//}

	//public override void OnStartClient(NetworkClient lobbyClient)
	//{
	//	this.playerPrefab = this.spawnPrefabs[lobbyClient.]
	//	base.OnStartClient(lobbyClient);

	//}

	//public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
	//{
	//	this.playerPrefab = this.spawnPrefabs[playerControllerId];
	//	return null;
	//}

	//[SerializeField]
	//Vector3 playerSpawnPos;
	//[SerializeField]
	//List<GameObject> characters;
	//// etc.

	//GameObject chosenCharacter; // character1, character2, etc.

	//// called when a new player is added for a client
	//public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	//{
	//	var player = (GameObject)GameObject.Instantiate(characters[playerControllerId], playerSpawnPos, Quaternion.identity);
	//       NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	//}

	//public class MsgTypes
	//{
	//	public const short PlayerPrefab = MsgType.Highest + 1;

	//	public class PlayerPrefabMsg : MessageBase
	//	{
	//		public short controllerID;
	//		public short prefabIndex;
	//	}
	//}

	//// in the Network Manager component, you must put your player prefabs 
	//// in the Spawn Info -> Registered Spawnable Prefabs section 
	//public short playerPrefabIndex;


	//public override void OnStartServer()
	//{
	//	NetworkServer.RegisterHandler(MsgTypes.PlayerPrefab, OnResponsePrefab);
	//	base.OnStartServer();
	//}

	//public override void OnClientConnect(NetworkConnection conn)
	//{
	//	client.RegisterHandler(MsgTypes.PlayerPrefab, OnRequestPrefab);
	//	base.OnClientConnect(conn);
	//}

	//private void OnRequestPrefab(NetworkMessage netMsg)
	//{
	//	MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
	//	msg.controllerID = netMsg.ReadMessage<MsgTypes.PlayerPrefabMsg>().controllerID;
	//	msg.prefabIndex = playerPrefabIndex;
	//	client.Send(MsgTypes.PlayerPrefab, msg);
	//}

	//private void OnResponsePrefab(NetworkMessage netMsg)
	//{
	//	MsgTypes.PlayerPrefabMsg msg = netMsg.ReadMessage<MsgTypes.PlayerPrefabMsg>();
	//	playerPrefab = spawnPrefabs[msg.prefabIndex];
	//	base.OnServerAddPlayer(netMsg.conn, msg.controllerID);
	//	Debug.Log(playerPrefab.name + " spawned!");
	//}

	//public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	//{
	//	MsgTypes.PlayerPrefabMsg msg = new MsgTypes.PlayerPrefabMsg();
	//	msg.controllerID = playerControllerId;
	//	NetworkServer.SendToClient(conn.connectionId, MsgTypes.PlayerPrefab, msg);
	//	base.OnServerAddPlayer(conn, playerControllerId);
	//}

	//// I have put a toggle UI on gameObjects called PC1 and PC2 to select two different character types.
	//// on toggle, this function is called, which updates the playerPrefabIndex
	//// The index will be the number from the registered spawnable prefabs that 
	//// you want for your player
	//public void UpdatePC()
	//{
	//	if (GameObject.Find("PC1").GetComponent<Toggle>().isOn)
	//	{
	//		playerPrefabIndex = 0;
	//	}
	//	//else if (GameObject.Find("PC2").GetComponent<Toggle>().isOn)
	//	//{
	//	//	playerPrefabIndex = 1;
	//	//}
	//	else
	//	{
	//		playerPrefabIndex = 1;
	//	}
	//}

	void Start()
	{
		s_Singleton = this;
		_lobbyHooks = GetComponent<Prototype.NetworkLobby.LobbyHook>();
		currentPanel = mainMenuPanel;

		backButton.gameObject.SetActive(false);
		GetComponent<Canvas>().enabled = true;

		DontDestroyOnLoad(gameObject);

		SetServerInfo("Offline", "None");
	}

//	void OnLevelWasLoaded()
//	{
////		ClientScene.AddPlayer(0);
//	}

	//void OnLevelWasLoaded()
	//{
	//	setAuthority();

	//	//GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
	//	//if (networkAuthorityGroup)
	//	//{
	//	//	AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
	//	//	if (assignPlayerAuthority)
	//	//	{
	//	//		for(int i = 0; i < lobbySlots.Length; ++i)
	//	//		{
	//	//			NetworkLobbyPlayer player = lobbySlots[i];
	// //                  assignPlayerAuthority.assignAuthority(player.slot, player.connectionToClient);
	// //              }
	//	//	}
	//	//}
	//}

	////[Command]
	//void setAuthority()
	//{
	//	GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
	//	if (networkAuthorityGroup)
	//	{
	//		AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
	//		if (assignPlayerAuthority)
	//		{
	//			foreach(NetworkConnection netConn in NetworkServer.connections)
	//			{
	//                   assignPlayerAuthority.assignAuthority(netConn.connectionId, netConn);
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

	//	////DontDestroyOnLoad(this);

	//	//GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
	//	//if (networkAuthorityGroup)
	//	//{
	//	//	AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
	//	//	if (assignPlayerAuthority)
	//	//	{
	//	//		assignPlayerAuthority.assignAuthority(playerNetID.clientAuthorityOwner.connectionId, playerNetID.clientAuthorityOwner);//alt: (playerId, playerNetID.connectionToClient);
	//	//	}
	//	//}
	//}
}