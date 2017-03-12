using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class SpawnPointManager : NetworkBehaviour
{
	public Transform groundPlayerSpawnPosition;
	public Transform airPlayerStartPosition;

	public List<Transform> enemySpawnPositions = new List<Transform>();

	public List<Transform> allySpawnPositions = new List<Transform>();

	public Vector3 getPlayerSpawnPosition(int playerId)
	{
		Vector3 spawnPosition = Vector3.zero;
		switch(playerId)
		{
			case (0):
			{
				spawnPosition = groundPlayerSpawnPosition.position;
                break;
			}
			case (1):
			{
				spawnPosition = airPlayerStartPosition.position;
				break;
			}
		}
		return spawnPosition;
	}

	void OnEnable()
	{
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
		//CmdInstantiateCustomPlayer(GetComponent<NetworkIdentity>());
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

		//		CmdSetAuthority(this.GetComponent<NetworkIdentity>());
		//	instantiateCustomPlayer(this.GetComponent<NetworkIdentity>());
		//}
	}

	//[Command]
	//public void CmdSpawnObject(NetworkIdentity netId)
	//{
	//	if (netId)
	//	{
	//		if (netId.connectionToServer != null)
	//		{
	//			GameObject gameObject = (GameObject)Instantiate()
	//			NetworkServer.SpawnWithClientAuthority(NetworkLobbyManager.singleton.spawnPrefabs[netId.connectionToServer.connectionId], this.gameObject);
	//		}
	//	}
	//}

	//[Command]
	//public void CmdInstantiateCustomPlayer(NetworkIdentity playerNetID)
	//{
	//	instantiateCustomPlayer(playerNetID);
	//}

	//private void instantiateCustomPlayer(NetworkIdentity playerNetID)
	//{
	//	Vector3 spawnPosition = getPlayerSpawnPosition(playerNetID.clientAuthorityOwner.connectionId);

	//	//GameObject playerObject = (GameObject)Network.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerNetID.clientAuthorityOwner.connectionId], spawnPosition, Quaternion.identity, 0);
	//	GameObject playerObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[playerNetID.clientAuthorityOwner.connectionId], spawnPosition, Quaternion.identity, this.transform);
	//	if (playerObject)
	//	{
	//		NetworkServer.AddPlayerForConnection(playerNetID.clientAuthorityOwner, playerObject, playerNetID.playerControllerId);
	//	}
	//}

	// Use this for initialization
	void Start()
	{
		//if(isClient)
		//{
		//	CmdInstantiateCustomPlayer(GetComponent<NetworkIdentity>());
		//}
	}

	// Update is called once per frame
	void Update()
	{

	}
}

