using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    bool spawning;

    void Start()
    {
        spawning = false;

        StartCoroutine(spawn());
    }

    void Update()
    {
        if(isServer && !spawning)
        {
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawn()
    {
        spawning = true;
        yield return new WaitForSeconds(10.0f);
        NetworkSpawnEntity nse = gameObject.GetComponent<NetworkSpawnEntity>();
        for(int i = 0; i < 5; i++)
        {
            nse.spawnEnemy();
        }
        spawning = false;
    }
}
