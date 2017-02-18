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

    private DebugEntities debug;


    //Flag for knowing we're in top down view
    bool topDown = false;

	void Start () {
		activateCamera (regularView);
		topDown = false;
		targetControl.gameObject.SetActive (topDown);

        debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<DebugEntities>();
    }
	
	void Update () {
        //Camera movement input
        float verticalInput;
        if (!debug.m_useWorldCam)
            verticalInput = Input.GetAxis("VerticalRightStick");
        else
            verticalInput = 0;


        float horizontalInput;
        if (!debug.m_useWorldCam)
            horizontalInput = Input.GetAxis("HorizontalRightStick");
        else
            horizontalInput = 0;

        //Move active camera
        if (!debug.m_useWorldCam)
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
        if (debug.m_useWorldCam)
            verticalLeftStick = Input.GetAxis("VerticalBird");
        else
            verticalLeftStick = Input.GetAxis("Vertical");

        float horizontalLeftStick;
        if (debug.m_useWorldCam)
            horizontalLeftStick = Input.GetAxis("HorizontalBird");
        else
            horizontalLeftStick = Input.GetAxis("Horizontal");

        moverBehaviour.updateInput (new Vector3 (verticalLeftStick,0, horizontalLeftStick));

		if ((debug.m_useWorldCam && Input.GetButtonDown ("JumpBird")) ||
            !debug.m_useWorldCam && Input.GetButtonDown("Jump")) {
			switchCameras ();
		}

		if (topDown) {
			if ((debug.m_useWorldCam && Input.GetButtonDown ("Fire1Bird")) ||
			    !debug.m_useWorldCam && Input.GetButtonDown ("Fire1")) {
				Debug.Log ("Hello mr activate");
				targetControl.activateCurrent ();
			} 
			else if ((debug.m_useWorldCam && Input.GetButtonUp ("Fire1Bird")) ||
				!debug.m_useWorldCam && Input.GetButtonUp ("Fire1")) {
				Debug.Log ("Hello mr disactivate");
				targetControl.disactivateCurrent ();
			}
			if ((debug.m_useWorldCam && Input.GetButtonDown("Fire2Bird")) ||
            !debug.m_useWorldCam && Input.GetButtonDown("Fire2")) {
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
