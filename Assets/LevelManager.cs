using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelManager : NetworkBehaviour {

    public GameObject tutorial;
    public GameObject threePuzzleLevel;
    public GameObject lastLevel;

    public Transform tutorialBirdSpawn;
    public Transform tutorialBearSpawn;

    public Transform threePuzzleBirdSpawn;
    public Transform threePuzzleBearSpawn;

    public Transform lastLevelBirdSpawn;
    public Transform lastlevelBearSpawn;

    public bool startAtFirstLevel;
    public bool startAtSecondLevel;

    GameObject spawnPointP1;
    GameObject spawnPointP2;
    

    bool doOnce;

    void Start()
    {
        doOnce = false;

        if(startAtFirstLevel)
        {
            tutorial.SetActive(true);
        }
        else if(startAtSecondLevel)
        {
            threePuzzleLevel.SetActive(true);
        }
        else
        {
            lastLevel.SetActive(true);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && isServer)
        {
            nextLevel();
        }
    }

    public void nextLevel()
    {
        if(isServer)
        {
            CmdNextLevel();
        }
    }

    [Command]
    public void CmdNextLevel()
    {
        RpcNextLevel();
    }

    [ClientRpc]
    public void RpcNextLevel()
    {
        GameObject.FindGameObjectWithTag("CameraEffects").GetComponent<CameraEffects>().transition();

        if (tutorial.active)
        {
            tutorial.SetActive(false);
            threePuzzleLevel.SetActive(true);

            spawnPointP1 = GameObject.Find("SpawnPoint_P1");
            spawnPointP2 = GameObject.Find("SpawnPoint_P2");

            GameObject p1 = GameObject.Find("GroundPlatformer(Clone)");
            for(int i = 0; i < p1.transform.GetChildCount(); i++)
            {
                p1.transform.GetChild(i).position = spawnPointP1.transform.position;
            }

            GameObject p2 = GameObject.Find("AirDrawer(Clone)");
            for (int i = 0; i < p2.transform.GetChildCount(); i++)
            {
                p2.transform.GetChild(i).position = spawnPointP2.transform.position;
            }


        }

        if (threePuzzleLevel.active)
        {
            threePuzzleLevel.SetActive(false);
            lastLevel.SetActive(true);

            spawnPointP1 = GameObject.Find("SpawnPoint_P1");
            spawnPointP2 = GameObject.Find("SpawnPoint_P2");

            GameObject p1 = GameObject.Find("GroundPlatformer(Clone)");
            for (int i = 0; i < p1.transform.GetChildCount(); i++)
            {
                p1.transform.GetChild(i).position = spawnPointP1.transform.position;
            }

            GameObject p2 = GameObject.Find("AirDrawer(Clone)");
            for (int i = 0; i < p2.transform.GetChildCount(); i++)
            {
                p2.transform.GetChild(i).position = spawnPointP2.transform.position;
            }
        }

        if (lastLevel.active)
        {
            GetComponent<DialogueTrigger>().forceSubmit();
        }
    }
}
