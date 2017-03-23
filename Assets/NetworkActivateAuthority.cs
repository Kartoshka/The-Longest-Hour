using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkActivateAuthority : NetworkBehaviour {

	public List<GameObject> activeOnlyAuthority;
	// Use this for initialization
	void Start () {
		if (this.isServer)
		{
			foreach (GameObject g in activeOnlyAuthority)
			{
				g.gameObject.SetActive (true);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
