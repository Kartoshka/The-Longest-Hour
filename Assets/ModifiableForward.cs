using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiableForward : MonoBehaviour {

	private Transform camTransform;

	public void rotate(float angle){
		Vector3 fwd = this.transform.forward;
		fwd = Quaternion.AngleAxis (angle, Vector3.up)*fwd;
		this.transform.forward = fwd;
	}

	// Update is called once per frame
	public void faceCamera () {
		GameObject cam = GameObject.FindGameObjectWithTag ("MainCamera");
		if (cam)
		{
			camTransform = cam.transform;
		}
		if (camTransform)
		{
			this.gameObject.transform.forward = new Vector3 (camTransform.forward.x,this.transform.forward.y, camTransform.forward.z).normalized;
		}
	}
}
