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

    private float m_moveInputThreshold = 0.5f;

    private DebugEntities debug;


    //Flag for knowing we're in top down view
    bool topDown = false;

	void Start () {
		activateCamera (regularView);
		topDown = false;
		targetControl.gameObject.SetActive (topDown);

		GameObject debugObject = GameObject.FindGameObjectWithTag("Debug");
		if (debugObject)
		{
			debug = debugObject.GetComponent<DebugEntities>();
		}
		else
		{
			Debug.LogWarning("No GameObject with the tag 'Debug' was found.");
		}
	}
	
	void Update () {
        //Camera movement input
        float verticalInput;
        if (debug && !debug.m_useWorldCam)
            verticalInput = Input.GetAxis("VerticalRightStick");
        else
            verticalInput = 0;


        float horizontalInput;
        if (debug && !debug.m_useWorldCam)
            horizontalInput = Input.GetAxis("HorizontalRightStick");
        else
            horizontalInput = 0;

        //Move active camera
        if (debug && !debug.m_useWorldCam)
        {
            active.increasePitch(-verticalInput);
            active.increaseYaw(horizontalInput);
            active.UpdatePosition();
        }
        else
        {
            deactivateCamera();
        }

        //Movement controls
        float verticalLeftStick;
        if (debug && debug.m_useWorldCam)
            verticalLeftStick = Input.GetAxis("VerticalBird");
        else
            verticalLeftStick = Input.GetAxis("Vertical");

        float horizontalLeftStick;
        if (debug && debug.m_useWorldCam)
            horizontalLeftStick = Input.GetAxis("HorizontalBird");
        else
            horizontalLeftStick = Input.GetAxis("Horizontal");

        if (Mathf.Abs(verticalLeftStick) < m_moveInputThreshold)
            verticalLeftStick = 0;
        if(Mathf.Abs(horizontalLeftStick) < m_moveInputThreshold)
            horizontalLeftStick = 0;

        moverBehaviour.updateInput (new Vector3 (verticalLeftStick,0, horizontalLeftStick));

		if (debug && (
			(debug.m_useWorldCam && Input.GetButtonDown ("JumpBird")) ||
            !debug.m_useWorldCam && Input.GetButtonDown("Jump"))) {
			switchCameras ();
		}

		if (topDown) {
			if (debug && (
				(debug.m_useWorldCam && Input.GetButtonDown ("Fire1Bird")) ||
			    !debug.m_useWorldCam && Input.GetButtonDown ("Fire1"))) {
				targetControl.activateCurrent ();
			} 
			else if (debug && (
				(debug.m_useWorldCam && Input.GetButtonUp ("Fire1Bird")) ||
				!debug.m_useWorldCam && Input.GetButtonUp ("Fire1"))) {
				targetControl.disactivateCurrent ();
			}
			if (debug && (
				(debug.m_useWorldCam && Input.GetButtonDown("Fire2Bird")) ||
				!debug.m_useWorldCam && Input.GetButtonDown("Fire2"))) {
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

    private void deactivateCamera()
    {
        active.gameObject.SetActive(false);
    }
}
