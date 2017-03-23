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

    public Transform m_l1Spawn;
    public Transform m_l2Spawn;
    public Transform m_ll1Spawn;
    public Transform m_ll2Spawn;
    public Transform m_ll3Spawn;
    public Transform m_ll4Spawn;

    private void spawnEntities(int networkPrefabIndex, List<Transform> spawnLocations)
    {
        for (int i = 0; i < spawnLocations.Count; ++i)
        {
            Transform transform = spawnLocations[i];
            if(transform == null)
            {
                continue;
            }
            GameObject gameObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[networkPrefabIndex], transform.position, transform.rotation, this.transform);
            NetworkServer.Spawn(gameObject);
        }
    }

    private void spawnEntity(int networkPrefabIndex, Transform spawnLocation)
    {
        if(spawnLocation == null)
        {
            return;
        }
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
            spawnEntity(11, m_l1Spawn);
            spawnEntity(12, m_l2Spawn);
            spawnEntity(13, m_ll1Spawn);
            spawnEntity(14, m_ll2Spawn);
            spawnEntity(15, m_ll3Spawn);
            spawnEntity(16, m_ll4Spawn);
        }
    }
    

}
