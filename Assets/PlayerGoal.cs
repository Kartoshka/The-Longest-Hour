using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGoal : MonoBehaviour {

    public bool lastLevel;
	
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
           
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().nextLevel();

            if(lastLevel)
            {
                Destroy(gameObject);
            }
        }
    }
}
