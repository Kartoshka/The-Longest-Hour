using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawnEntity : NetworkBehaviour
{
	public int enemyPrefabNetworkIndex = 2;
	public List<Transform> m_enemySpawnLocations;
	public int maxEnemies;

	// Use this for initialization
	void Start ()
	{
		if(isServer)
		{
			for(int i = 0; i < maxEnemies; ++i)
			{
				Transform transform = m_enemySpawnLocations[i % m_enemySpawnLocations.Count];
				GameObject gameObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[enemyPrefabNetworkIndex], transform.position, transform.rotation, this.transform);
				NetworkServer.Spawn(gameObject);
            }
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
