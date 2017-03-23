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

    public bool invertX;
    public bool invertY;

    //Flag for knowing we're in top down view
    bool topDown = false;

	void Start () {
		activateCamera (regularView);
		topDown = false;
		targetControl.gameObject.SetActive (topDown);

        targetControl.gameObject.SetActive(true);
    }
	
	void Update ()
    {

        float XaxisInvert = 1.0f;
        float YaxisInvert = 1.0f;
       
        if(invertX)
        {
            XaxisInvert = -1.0f;
        }

        if(invertY)
        {
            YaxisInvert = -1.0f;
        }

        //Movement controls
        float verticalLeftStick = Input.GetAxis("Vertical") ;

        float horizontalLeftStick = Input.GetAxis("Horizontal");

        if (Mathf.Abs(verticalLeftStick) < m_moveInputThreshold)
            verticalLeftStick = 0;
        if(Mathf.Abs(horizontalLeftStick) < m_moveInputThreshold)
            horizontalLeftStick = 0;

        moverBehaviour.updateInput (new Vector3 (verticalLeftStick,0, horizontalLeftStick));

        float rightTrigger = Input.GetAxis("RightTrigger");
        float leftTrigger = Input.GetAxis("LeftTrigger");

		float verticalRightStick = Input.GetAxis ("VerticalRightStick") * YaxisInvert;
		float horizontalRightStick = Input.GetAxis ("HorizontalRightStick") * XaxisInvert;
		if (active)
		{
			active.increasePitch (verticalRightStick);
			active.increaseYaw (horizontalRightStick);
			active.UpdatePosition ();
		}


        // ability controls
        /*
		if (Input.GetButtonDown("Jump")) {
			switchCameras ();
            targetControl.toggle();
        }

		if (topDown) {
			if (Input.GetButtonDown ("Fire1")) {
				targetControl.activateCurrent ();
			} 
			else if (Input.GetButtonUp ("Fire1")) {
				targetControl.disactivateCurrent ();
			}
			if (Input.GetButtonDown("Fire2")) {
				targetControl.toggle ();
			}
		}
        */

        // hold left trigger to go top down view
        if (!topDown && leftTrigger == 1)
        {
            switchCameras();
        }
        else if (topDown && leftTrigger == 0)
        {
            switchCameras();
        }

        // press right trigger, draw
        if(rightTrigger == 1)
        {
            targetControl.toggleDraw();
            targetControl.activateCurrent();
        }
        // if detected divable object, press x to dive to it
        else if (Input.GetButtonDown("Fire3"))
        {
            if (topDown)
            {
                switchCameras();
            }
            targetControl.toggleDive();
            targetControl.activateCurrent();
        }
        // y for beacon
        else if (Input.GetButtonDown("Jump"))
        {
            
            targetControl.toggleBeacon();
            targetControl.activateCurrent();
        }
        // otherwise, reset abilities
        else
        {
            targetControl.disactivateCurrent();
        }


        // if holding object, press a to let go? (bcus we might want to dive under something while holding obj?)
        //if (Input.GetButtonDown("Fire1"))
        //{

        //}
        
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
