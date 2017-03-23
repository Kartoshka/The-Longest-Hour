using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Capturable : MonoBehaviour {

	protected bool captured = false;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        Release ();
    }
	
	public void Capture(){
		if (!captured) {
			captured = true;
			rb.isKinematic = true;
		}
	}

	public void Release(){
		if (captured) {
			captured = false;
            // lock the angles of the object so it doesn't rotate mid-air, but then it collides with the bird, so figure something out!!
            //rb.velocity = new Vector3(0, 0, 0); 
            //rb.angularVelocity = new Vector3(0, 0, 0);

            rb.isKinematic = false;
		}
	}
}
