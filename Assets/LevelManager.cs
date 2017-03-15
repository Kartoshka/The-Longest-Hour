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
    

    void Start()
    {
        tutorial.SetActive(true);
    }

    public void nextLevel()
    {
        if(tutorial.active)
        {
            tutorial.SetActive(false);
            threePuzzleLevel.SetActive(true);
        }

        if(threePuzzleLevel.active)
        {
            threePuzzleLevel.SetActive(false);
            lastLevel.SetActive(true);
        }
    }

}
