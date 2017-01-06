using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {

	public ThirdPersonCamera cam;

	public float duration;
	public Vector2 rotation;
	private Vector2 increment;

	void Start(){
		increment = new Vector2 (rotation.x / duration, rotation.y / duration);
	}

	// Update is called once per frame
	void Update () {
		if (duration <= 0) {
			
		}
		
	}
}
