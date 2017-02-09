using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
        float verticalInput;
        if (Network.peerType == NetworkPeerType.Disconnected)
            verticalInput = Input.GetAxis("VerticalRightStick");
        else
            verticalInput = 0;


        float horizontalInput;
        if (Network.peerType == NetworkPeerType.Disconnected)
            horizontalInput = Input.GetAxis("HorizontalRightStick");
        else
            horizontalInput = 0;

        //Move active camera
        active.increasePitch (-verticalInput);
		active.increaseYaw (horizontalInput);
		active.UpdatePosition ();

        //Movement controls
        float verticalLeftStick;
        if (Network.peerType == NetworkPeerType.Disconnected)
            verticalLeftStick = Input.GetAxis("VerticalBird");
        else
            verticalLeftStick = Input.GetAxis("Vertical");

        float horizontalLeftStick;
        if (Network.peerType == NetworkPeerType.Disconnected)
            horizontalLeftStick = Input.GetAxis("HorizontalBird");
        else
            horizontalLeftStick = Input.GetAxis("Horizontal");

        moverBehaviour.updateInput (new Vector3 (verticalLeftStick,0, horizontalLeftStick));

		if ((Network.peerType == NetworkPeerType.Disconnected && Input.GetButtonDown ("JumpBird")) ||
            Network.peerType != NetworkPeerType.Disconnected && Input.GetButtonDown("Jump")) {
			switchCameras ();
		}

		if (topDown) {
			if ((Network.peerType == NetworkPeerType.Disconnected && Input.GetButtonDown("Fire1Bird")) ||
            Network.peerType != NetworkPeerType.Disconnected && Input.GetButtonDown("Fire1")) {
				targetControl.TriggerCurrent ();
			}
			if ((Network.peerType == NetworkPeerType.Disconnected && Input.GetButtonDown("Fire2Bird")) ||
            Network.peerType != NetworkPeerType.Disconnected && Input.GetButtonDown("Fire2")) {
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
