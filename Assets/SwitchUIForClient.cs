using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SwitchUIForClient : NetworkBehaviour {

    public GameObject bearHudTop;
    public GameObject bearHudBottom;

    public GameObject birdHudTop;
    public GameObject birdHudBottom;

	// Use this for initialization
	void Start ()
    {
		if(isServer)
        {
            bearHudTop.SetActive(true);
            bearHudBottom.SetActive(true);
        }
        else
        {
            birdHudTop.SetActive(true);
            birdHudBottom.SetActive(true);
        }
	}
	
}
