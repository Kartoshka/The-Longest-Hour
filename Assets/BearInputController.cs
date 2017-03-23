using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearInputController : MonoBehaviour {

	private DebugEntities debug;

//	public CameraController bearCameras;
	public BearController m_bearControls;
	public float m_timeScale = 0.02f;

    public bool invertX;
    public bool invertY;

	private float m_moveInputThreshold = 0.5f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		processInputs();
	}

	private void processInputs()
	{
		float XaxisInvert = 1.0f;
		float YaxisInvert = 1.0f;

		if (invertX)
		{
			XaxisInvert = -1.0f;
		}

		if (invertY)
		{
			YaxisInvert = -1.0f;
		}
		//Camera movement input
		float verticalInput;
		//		if (debug && !debug.m_useWorldCam)
		verticalInput = Input.GetAxis("VerticalRightStick") * YaxisInvert;
		//		else
		//			verticalInput = 0;


		float horizontalInput;
		//		if (debug && !debug.m_useWorldCam)
		horizontalInput = Input.GetAxis("HorizontalRightStick") * XaxisInvert;
		//		else
		//			horizontalInput = 0;

		m_bearControls.m_camControlers.updateCamera(new Vector2(horizontalInput, verticalInput));
		//		bearCameras.getActive().increasePitch(-verticalInput);
		//		bearCameras.getActive().increaseYaw(horizontalInput);
		//		bearCameras.getActive().UpdatePosition();

		//Movement controls
		float verticalLeftStick;
		if (debug && debug.m_useWorldCam)
			verticalLeftStick = Input.GetAxis("VerticalBear");
		else
			verticalLeftStick = Input.GetAxis("Vertical");

		float horizontalLeftStick;
		if (debug && debug.m_useWorldCam)
			horizontalLeftStick = Input.GetAxis("HorizontalBird");
		else
			horizontalLeftStick = Input.GetAxis("Horizontal");

		if (Mathf.Abs(verticalLeftStick) < m_moveInputThreshold)
			verticalLeftStick = 0;
		if (Mathf.Abs(horizontalLeftStick) < m_moveInputThreshold)
			horizontalLeftStick = 0;

		if (Input.GetButtonDown("Fire1"))
		{
			m_bearControls.doAttack();
		}

		if (Input.GetButton("AimBear"))
		{
			m_bearControls.startAim();
		}
		else
		{
			m_bearControls.stopAim();
		}


		if (Input.GetButton("Fire1"))
		{
			m_bearControls.fireGrenade();
		}

		bool run = Input.GetButton("Sprint");

		float rTrgr = Mathf.Clamp01(Input.GetAxis("RightTrigger"));
		float lTrgr = Mathf.Clamp01(Input.GetAxis("LeftTrigger"));
		setTime(rTrgr - lTrgr);

		m_bearControls.locateOther(Input.GetButton("LocateOther"));
		m_bearControls.moveBear(new Vector3(verticalLeftStick, 0, horizontalLeftStick), run);
	}

	public void processInputs(bool isFiring, bool isAiming)
	{
		if (isFiring)
		{
			m_bearControls.doAttack();
		}

		if (isAiming)
		{
			m_bearControls.startAim();
		}
		else
		{
			m_bearControls.stopAim();
		}
	}

	public void setTime(float time)
	{
		m_bearControls.setTime(time * m_timeScale);
	}
}
