using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawnEntity : NetworkBehaviour
{
	public int enemyPrefabNetworkIndex = 2;
	public List<Transform> m_enemySpawnLocations;

	public int trashbinPrefabNetworkIndex = 4;
	public List<Transform> m_trashbinSpawnLocations;

	public int cratePrefabNetworkIndex = 3;
	public List<Transform> m_crateSpawnLocations;

	private void spawnEntities(int networkPrefabIndex, List<Transform> spawnLocations)
	{
		for (int i = 0; i < m_enemySpawnLocations.Count; ++i)
		{
			Transform transform = m_enemySpawnLocations[i];
			GameObject gameObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[networkPrefabIndex], transform.position, transform.rotation, this.transform);
			NetworkServer.Spawn(gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		if(isServer)
		{
			spawnEntities(enemyPrefabNetworkIndex, m_enemySpawnLocations);
			spawnEntities(trashbinPrefabNetworkIndex, m_trashbinSpawnLocations);
			spawnEntities(cratePrefabNetworkIndex, m_crateSpawnLocations);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
