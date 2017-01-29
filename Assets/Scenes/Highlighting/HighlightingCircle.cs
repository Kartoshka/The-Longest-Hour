using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightingCircle : MonoBehaviour {

    public KeyCode triggerKey;

    private bool heldDown;
    
    void Start()
    {
        heldDown = false;
    }

	// Update is called once per frame
	void Update ()
    {
	    if(!heldDown && Input.GetKeyDown(triggerKey))
        {
            heldDown = true;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                obj.GetComponent<CircleTrail>().enabled = true;
            }

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Objective"))
            {
                obj.GetComponent<CircleTrail>().enabled = true;
            }
        }

        if(Input.GetKeyUp(triggerKey))
        {
            heldDown = false;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                obj.GetComponent<CircleTrail>().enabled = false;
            }

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Objective"))
            {
                obj.GetComponent<CircleTrail>().enabled = false;
            }
        }
	}
}
