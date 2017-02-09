using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCam : MonoBehaviour {

    public Camera camA;
    public Camera camB;

    private bool heldDown;

    void Start()
    {
        heldDown = false;
    }

	// Update is called once per frame
	void Update ()
    {
	    if(!heldDown && Input.GetKeyDown(KeyCode.Space))
        {
            heldDown = true;
            camA.enabled = false;
            camB.enabled = true;
            
        }	

        if(Input.GetKeyUp(KeyCode.Space))
        {
            heldDown = false;
            camB.enabled = false;
            camA.enabled = true;
        }
	}
}
