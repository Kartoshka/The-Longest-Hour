using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightingCircles : MonoBehaviour {

    public KeyCode triggerKey;
    private bool heldDown;

    void Start()
    {
        heldDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!heldDown && Input.GetKeyDown(triggerKey))
        {
            heldDown = true;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("CircleBottom"))
            {
                obj.GetComponentInChildren<MeshRenderer>(true).enabled = true;
            }
        }

        if (Input.GetKeyUp(triggerKey))
        {
            heldDown = false;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("CircleBottom"))
            {
                obj.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }
    }
}
