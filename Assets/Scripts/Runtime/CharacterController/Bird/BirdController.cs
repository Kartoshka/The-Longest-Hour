using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour {

	[Header("Cameras")]
	public Cinemachine3rdPerson regularView;	
	public Cinemachine3rdPerson topDownView;

	//Active camera being modified
	Cinemachine3rdPerson active;

	[Space(5)]
	[Tooltip("Character movement")]
	public InputVelocityMoverBehaviour moverBehaviour;

	[Space(5)]
	[Header("Targeting controls")]
	public TargetingController targetControl;


	//Flag for knowing we're in top down view
	bool topDown = false;

	void Start () {
		activateCamera (regularView);
		topDown = false;
		targetControl.gameObject.SetActive (topDown);
	}
	
	void Update () {
		//Camera movement input
		float verticalInput = Input.GetAxis ("VerticalRightStick");
		float horizontalInput = Input.GetAxis ("HorizontalRightStick");

		//Move active camera
		active.increasePitch (-verticalInput);
		active.increaseYaw (horizontalInput);
		active.UpdatePosition ();

		//Movement controls
		float verticalLeftStick = Input.GetAxis ("Vertical");
		float horizontalLeftStick = Input.GetAxis ("Horizontal");
		moverBehaviour.updateInput (new Vector3 (verticalLeftStick,0, horizontalLeftStick));

		if (Input.GetButtonDown ("Jump")) {
			switchCameras ();
		}

		if (topDown) {
			if (Input.GetButtonDown ("Fire1")) {
				targetControl.TriggerCurrent ();
			}
			if (Input.GetButtonDown ("Fire2")) {
				targetControl.toggle ();
			}
		}

	}

	private void switchCameras()
	{
		if (active == regularView) {
			topDown = !topDown;
			activateCamera (topDownView);

		} else  {
			topDown = !topDown;
			activateCamera (regularView);
		}

		targetControl.gameObject.SetActive (topDown);

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
