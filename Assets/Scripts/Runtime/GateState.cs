using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GateState : NetworkBehaviour {

    public GameObject redKey;
    public GameObject blueKey;
    public GameObject greenKey;
    public GameObject yellowKey;
    
    [SyncVar]
    int numLocks;

	// Use this for initialization
	void Start ()
    {
        CmdSetLockCount(4);	
	}

    [Command]
    void CmdSetLockCount(int lockCount)
    {
        numLocks = lockCount;
    }

    [Command]
    void CmdDecreaseLockCount()
    {
        numLocks--;
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

        CmdDecreaseLockCount();

    }

    void Update()
    {
        if (numLocks == 0)
        {
            Destroy(gameObject);
        }
    }
}
