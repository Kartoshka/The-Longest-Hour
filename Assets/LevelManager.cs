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

        tutorial.SetActive(true);
        threePuzzleLevel.SetActive(true);
        lastLevel.SetActive(true);
        StartCoroutine(requiredDelay());
    }

    IEnumerator requiredDelay()
    {
        yield return new WaitForSeconds(3.0f);
        if(isServer)
        {
            CmdDisableNonTutorials();
        }
    }

    [Command]
    void CmdDisableNonTutorials()
    {
        RpcDisableNonTutorials();
    }

    [ClientRpc]
    void RpcDisableNonTutorials()
    {
        threePuzzleLevel.SetActive(false);
        lastLevel.SetActive(false);

        spawnPointP1 = GameObject.Find("SpawnPoint_P1_1");
        spawnPointP2 = GameObject.Find("SpawnPoint_P2_1");

        GameObject p1 = GameObject.Find("GroundPlatformer(Clone)");
        for (int i = 0; i < p1.transform.GetChildCount(); i++)
        {
            p1.transform.GetChild(i).position = spawnPointP1.transform.position;
        }

		TimeShiftController timeShiftController = p1.GetComponentInChildren<TimeShiftController>();
		if(timeShiftController)
		{
			timeShiftController.findParamControllers();
        }

		GameObject p2 = GameObject.Find("AirDrawer(Clone)");
        for (int i = 0; i < p2.transform.GetChildCount(); i++)
        {
            p2.transform.GetChild(i).position = spawnPointP2.transform.position;
        }

		timeShiftController = p2.GetComponentInChildren<TimeShiftController>();
		if (timeShiftController)
		{
			timeShiftController.findParamControllers();
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


        }else if (threePuzzleLevel.active)
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
