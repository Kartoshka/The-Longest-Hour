using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class NetworkedCameraController :  NetworkBehaviour{

	public CameraController m_camController;
	// Use this for initialization
	void Start () {
		if (m_camController)
		{
			m_camController.gameObject.SetActive (this.hasAuthority);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
