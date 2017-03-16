using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearInputController : MonoBehaviour {

	private DebugEntities debug;

	public CameraController bearCameras;
	public BearController m_bearControls;

	private float m_moveInputThreshold = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Camera movement input
		float verticalInput;
//		if (debug && !debug.m_useWorldCam)
			verticalInput = Input.GetAxis("VerticalRightStick");
//		else
//			verticalInput = 0;


		float horizontalInput;
//		if (debug && !debug.m_useWorldCam)
			horizontalInput = Input.GetAxis("HorizontalRightStick");
//		else
//			horizontalInput = 0;

		//Move active camera
		if (bearCameras.getActive())
		{
			bearCameras.getActive().increasePitch(-verticalInput);
			bearCameras.getActive().increaseYaw(horizontalInput);
			bearCameras.getActive().UpdatePosition();
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

		if (Input.GetButtonDown ("Fire1"))
		{
			m_bearControls.doAttack ();
		} 

		if (Input.GetAxis("AimBear") > 0.001f)
		{
			m_bearControls.startAim ();
		} else
		{
			m_bearControls.stopAim ();
		}


		if(Input.GetButton("Fire1")){
			m_bearControls.fireGrenade ();
		}
		m_bearControls.moveBear (new Vector3 (verticalLeftStick, 0, horizontalLeftStick));

	
	}
}
