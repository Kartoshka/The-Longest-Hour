using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareAt : MonoBehaviour {

	public Transform a_Beacon;
	public Transform p_Camera;

	public bool lookAtCamera;
	public bool lookAtBecon;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lookAtCamera) { 	
			transform.LookAt (transform.position + p_Camera.transform.rotation * Vector3.forward, p_Camera.transform.rotation * Vector3.up);
		}

		if (lookAtBecon) {
			transform.LookAt (a_Beacon.position);
		}
	}
}
