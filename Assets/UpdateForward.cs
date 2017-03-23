using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateForward : MonoBehaviour {

	public float threshold = 0.1f;
	public bool freezeX, freezeY, freezeZ;
	private Vector3 lastPos;
	// Use this for initialization
	void Start () {
		lastPos = this.gameObject.transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movDifference = this.transform.position - lastPos;
		if (movDifference.magnitude > threshold)
		{
			this.gameObject.transform.forward = new Vector3 (freezeX?this.transform.forward.x:movDifference.x, freezeY?this.transform.forward.y:movDifference.y, freezeZ?this.transform.forward.z:movDifference.z).normalized;
		}
		lastPos = this.transform.position;
	}
}
