using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighting : MonoBehaviour {

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
                obj.GetComponent<MeshRenderer>().material.SetInt("_IsHighlighted", 1);
                obj.GetComponent<MeshRenderer>().material.SetColor("_HighlightColor", Color.red);
            }

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Objective"))
            {
                obj.GetComponent<MeshRenderer>().material.SetInt("_IsHighlighted", 1);
                obj.GetComponent<MeshRenderer>().material.SetColor("_HighlightColor", Color.blue);
            }
        }

        if(Input.GetKeyUp(triggerKey))
        {
            heldDown = false;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                obj.GetComponent<MeshRenderer>().material.SetInt("_IsHighlighted", 0);
            }

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Objective"))
            {
                obj.GetComponent<MeshRenderer>().material.SetInt("_IsHighlighted", 0);
            }
        }
	}
}
