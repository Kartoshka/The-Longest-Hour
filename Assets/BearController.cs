using UnityEngine;
using System;
using System.Collections;

public class BearController : MonoBehaviour {

	//Active camera being modified
	public Cinemachine3rdPerson bearCam;

	[Space(5)]
	[Tooltip("Character movement")]
	//The mover behaviour we're transferring stick information to
	public InputVelocityMoverBehaviour moverBehaviour;
	//Move inut threshold, gets rid of weird stick issues
	private float m_moveInputThreshold = 0.5f;

	[Space(5)]
	[Tooltip("Animation")]
	private DebugEntities debug;
	//Animator we're using
	public Animator m_animator;

	//String for name of attack trigger
	public string m_attackParam;
	//String for name of float that controls movement in animation
	public string m_runSpeedParam;

	[Space(5)]
	[Tooltip("Aiming")]



	//flag for aiming
	bool aiming = false;

	void Start () {
		debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<DebugEntities>();
		if(debug == null)
		{
			Debug.LogWarning("No GameObject with the tag 'Debug' was found.");
		}
	}

	void Update () {

		//Update active camera
		if (Input.GetButtonDown ("Fire2")) {
			aiming = true;

		} else if (Input.GetButtonUp ("Fire2")) {
			aiming = false;
		}

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
		if (debug && !debug.m_useWorldCam && bearCam != null)
		{
			bearCam.increasePitch(-verticalInput);
			bearCam.increaseYaw(horizontalInput);
			bearCam.UpdatePosition();
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


		//Bear paw swipe
		if (Input.GetButtonDown ("Fire1")) {
			m_animator.SetBool (m_attackParam, true);
	
		} else if(m_animator.GetBool("idle") && !aiming){
			float normalizedRunSpeed = Math.Abs(verticalLeftStick) + Math.Abs(horizontalLeftStick);
			m_animator.SetFloat(m_runSpeedParam, normalizedRunSpeed);
			moverBehaviour.updateInput (new Vector3 (verticalLeftStick, 0, horizontalLeftStick));
		}else{
			moverBehaviour.updateInput (Vector3.zero);
			m_animator.SetFloat(m_runSpeedParam, 0);
		}

	}


	private void enableAiming(){
	
	}

	private void resetDefautlCamera(){
		//Save initial settings for the cinemachine 3rd person, restore them 	
	}

	private void disableAiming(){
	
	}

}
