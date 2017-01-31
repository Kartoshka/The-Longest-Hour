using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {


	public Cinemachine3rdPerson regularView;	
	public Cinemachine3rdPerson topDownView;

	Cinemachine3rdPerson active;
	// Use this for initialization
	void Start () {
		activateCamera (regularView);
	}
	
	// Update is called once per frame
	void Update () {
		float verticalInput = Input.GetAxis ("Vertical");
		float horizontalInput = Input.GetAxis ("Horizontal");

		active.increasePitch (-verticalInput);
		active.increaseYaw (horizontalInput);

		active.UpdatePosition ();

		if (Input.GetButtonDown ("Jump")) {
			switchCameras ();
		}
	}


	private void switchCameras()
	{
		if (active == regularView) {
			activateCamera (topDownView);
		} else  {
			activateCamera (regularView);
		}
	}

	private void activateCamera(Cinemachine3rdPerson cam)
	{
		if (active != null) {
			active.gameObject.SetActive (false);
		}
		active = cam;
		active.gameObject.SetActive (true);
	}
}
