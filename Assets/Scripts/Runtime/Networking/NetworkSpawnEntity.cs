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
		for (int i = 0; i < spawnLocations.Count; ++i)
		{
			Transform transform = spawnLocations[i];
			GameObject gameObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[networkPrefabIndex], transform.position, transform.rotation, this.transform);
			NetworkServer.Spawn(gameObject);
		}
	}

    public void spawnEnemy()
    {
        Transform transform = m_enemySpawnLocations[Random.Range(0, m_enemySpawnLocations.Count)];
        GameObject gameObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[2], transform.position, transform.rotation, this.transform);
        NetworkServer.Spawn(gameObject);
    }

	public void spawnCrates()
	{
		if (isServer)
		{
			spawnEntities(cratePrefabNetworkIndex, m_crateSpawnLocations);
		}
	}

	public void spawnTrashBins()
	{
		if (isServer)
		{
			spawnEntities(trashbinPrefabNetworkIndex, m_trashbinSpawnLocations);
		}
	}

	public void spawnEnemies()
	{
		if (isServer)
		{
			spawnEntities(enemyPrefabNetworkIndex, m_enemySpawnLocations);
		}
	}

	// Use this for initialization
	void Start ()
	{
		if(isServer)
		{
			spawnEnemies();
			spawnTrashBins();
			spawnCrates();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
