using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Capturable : MonoBehaviour {

	protected bool captured = false;

	// Use this for initialization
	void Start () {
		Release ();
	}
	
	public void Capture(){
		if (!captured) {
			captured = true;
			this.GetComponent<Rigidbody> ().isKinematic = true;
		}
	}

	public void Release(){
		if (captured) {
			captured = false;
			this.GetComponent<Rigidbody> ().isKinematic = false;
		}
	}
}
