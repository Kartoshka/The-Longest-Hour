using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {


	public Cinemachine3rdPerson regularView;	
	public Cinemachine3rdPerson topDownView;

	public InputVelocityMoverBehaviour moverBehaviour;

	Cinemachine3rdPerson active;
	// Use this for initialization
	void Start () {
		activateCamera (regularView);
	}
	
	// Update is called once per frame
	void Update () {
		float verticalInput = Input.GetAxis ("VerticalRightStick");
		float horizontalInput = Input.GetAxis ("HorizontalRightStick");

		active.increasePitch (-verticalInput);
		active.increaseYaw (horizontalInput);

		active.UpdatePosition ();

		if (Input.GetButtonDown ("Jump")) {
			switchCameras ();
		}

		float verticalLeftStick = Input.GetAxis ("Vertical");
		float horizontalLeftStick = Input.GetAxis ("Horizontal");

		moverBehaviour.updateInput (new Vector2 (verticalLeftStick, horizontalLeftStick));

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
