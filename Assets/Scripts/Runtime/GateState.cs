using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateState : MonoBehaviour {

    public GameObject redKey;
    public GameObject blueKey;
    public GameObject greenKey;
    public GameObject yellowKey;

    int numLocks;

	// Use this for initialization
	void Start ()
    {
        numLocks = 4;	
	}

    public void disableLock(int key)
    {
        numLocks--;

        switch (key)
        {
            case (0):
                Destroy(redKey);
                break;
            case (1):
                Destroy(blueKey);
                break;
            case (2):
                Destroy(greenKey);
                break;
            case (3):
                Destroy(yellowKey);
                break;
        }

        if(numLocks == 0)
        {
            Destroy(gameObject);
        }
    }
}
