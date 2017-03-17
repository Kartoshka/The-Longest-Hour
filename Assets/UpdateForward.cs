using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateForward : MonoBehaviour {

	public float threshold = 0.1f;
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
			this.gameObject.transform.forward = new Vector3 (movDifference.x, 0, movDifference.z).normalized;
		}
		lastPos = this.transform.position;
		
	}
}
