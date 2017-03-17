using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCameraForward : MonoBehaviour {

	private Transform camTransform;
	// Use this for initialization
	void Start () {
		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		if (cam)
		{
			camTransform = cam.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (camTransform)
		{
			this.gameObject.transform.forward = new Vector3 (camTransform.forward.x, 0, camTransform.forward.z).normalized;
		}
	}
}
