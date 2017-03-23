using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkActivateAuthority : NetworkBehaviour {

	public List<GameObject> activeOnlyAuthority;
	public bool isBear;
	// Use this for initialization
	void Start () {

		foreach (GameObject g in activeOnlyAuthority)
		{
			g.gameObject.SetActive ((this.isServer && isBear) || (!this.isServer && !isBear));
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
