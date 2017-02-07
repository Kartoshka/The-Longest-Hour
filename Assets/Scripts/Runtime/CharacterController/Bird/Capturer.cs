using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capturer : MonoBehaviour {

	public bool containing{
		get{ return captured == null; }
	}
	private Capturable captured;

	public void Capture(Capturable c){
		if (captured == null) {
			captured = c;
			c.Capture ();
		}
	}

	public void Release(){
		if (captured != null) {
			captured.Release ();
			captured = null;
		}
	}

	public void Update(){
		if (containing) {
			captured.transform.position = this.transform.position;
		}
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.GetComponent<Capturable> () != null) {
			
		}
	}
}
