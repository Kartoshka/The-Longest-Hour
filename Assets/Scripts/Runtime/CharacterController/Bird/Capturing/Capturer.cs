using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturer : MonoBehaviour {

	public bool containing{
		get{ return captured != null; }
	}
	public Capturable captured;
    public float dropOffset = 0;

	public void Capture(Capturable c){
		if (captured == null) {
			captured = c;
			c.Capture ();
		}
	}

	public void Release(){
		if (captured != null) {
            captured.transform.position = this.gameObject.transform.position - Vector3.up* dropOffset;
            captured.Release ();
			captured = null;
		}
	}

	public void Update(){
		if (containing) {
			captured.transform.position = this.transform.position;
		}
	}
//
//	void OnTriggerEnter(Collider c){
//		if (c.gameObject.GetComponent<Capturable> () != null) {
//			
//		}
//	}
}
