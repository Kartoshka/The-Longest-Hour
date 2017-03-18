using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCameraForward : MonoBehaviour {

	private Transform camTransform;
	public bool freezeX;
	public bool freezeY;
	public bool freezeZ;
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
			this.gameObject.transform.forward = new Vector3 (freezeX?this.transform.forward.x:camTransform.forward.x, freezeY?this.transform.forward.y:camTransform.forward.y, freezeZ?this.transform.forward.z:camTransform.forward.z).normalized;
		}
	}
}
