using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkSpawnSpecific : NetworkBehaviour {

    public int blockSpawnNetworkIndex = 2;
    public Transform m_blockSpawn;

    public int ballSpawnNetworkIndex = 4;
    public List<Transform> m_ballSpawns;

    public int gateNetworkIndex = 3;
    public Transform m_gateSpawn;

    private void spawnEntities(int networkPrefabIndex, List<Transform> spawnLocations)
    {
        for (int i = 0; i < spawnLocations.Count; ++i)
        {
            Transform transform = spawnLocations[i];
            GameObject gameObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[networkPrefabIndex], transform.position, transform.rotation, this.transform);
            NetworkServer.Spawn(gameObject);
        }
    }

    private void spawnEntity(int networkPrefabIndex, Transform spawnLocation)
    {
        Transform transform = spawnLocation;
        GameObject gameObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[networkPrefabIndex], transform.position, transform.rotation, this.transform);
        NetworkServer.Spawn(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            spawnEntity(blockSpawnNetworkIndex, m_blockSpawn);
            spawnEntity(gateNetworkIndex, m_gateSpawn);
            spawnEntities(ballSpawnNetworkIndex, m_ballSpawns);
        }
    }
    
}
