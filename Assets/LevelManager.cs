using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

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

    public void nextLevel()
    {
        GameObject.FindGameObjectWithTag("CameraEffects").GetComponent<CameraEffects>().transition();

        if(tutorial.active)
        {
            tutorial.SetActive(false);
            threePuzzleLevel.SetActive(true);

            spawnPointP1 = GameObject.Find("SpawnPoint_P1");
            spawnPointP2 = GameObject.Find("SpawnPoint_P2");
            //spawnPointP1.transform.position = threePuzzleBearSpawn.transform.position;
            //spawnPointP2.transform.position = threePuzzleBirdSpawn.transform.position;
        }

        if(threePuzzleLevel.active)
        {
            threePuzzleLevel.SetActive(false);
            lastLevel.SetActive(true);

            spawnPointP1 = GameObject.Find("SpawnPoint_P1");
            spawnPointP2 = GameObject.Find("SpawnPoint_P2");
            //spawnPointP1.transform.position = lastlevelBearSpawn.transform.position;
            //spawnPointP2.transform.position = lastLevelBirdSpawn.transform.position;
        }
    }

    void LateUpdate()
    {
        if(doOnce)
        {
            doOnce = true;
            spawnPointP1 = GameObject.Find("SpawnPoint_P1");
            spawnPointP2 = GameObject.Find("SpawnPoint_P2");
            if (startAtFirstLevel)
            {
                spawnPointP1.transform.position = tutorialBearSpawn.transform.position;
                spawnPointP2.transform.position = tutorialBirdSpawn.transform.position;
            }
            else if (startAtSecondLevel)
            {
                spawnPointP1.transform.position = threePuzzleBearSpawn.transform.position;
                spawnPointP2.transform.position = threePuzzleBirdSpawn.transform.position;
            }
            else
            {
                spawnPointP1.transform.position = lastlevelBearSpawn.transform.position;
                spawnPointP2.transform.position = lastLevelBirdSpawn.transform.position;
            }
        }
    }

}
