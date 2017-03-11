using UnityEngine;
using System;
using System.Collections;

public class BearController : MonoBehaviour {

	//Active camera being modified
	public Cinemachine3rdPerson bearCam;

	[Space(5)]
	[Tooltip("Character movement")]
	public InputVelocityMoverBehaviour moverBehaviour;

	private float m_moveInputThreshold = 0.5f;

	[Space(5)]
	[Tooltip("Animation")]
	private DebugEntities debug;
	public Animator m_animator;


	public string m_attackParam;
	public string m_runSpeedParam;


	//Flag for knowing we're in top down view
	bool topDown = false;

	void Start () {
		topDown = false;
		debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<DebugEntities>();
		if(debug == null)
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
		if (Input.GetButtonDown ("Fire1") && !m_animator.GetBool(m_attackParam)) {
			m_animator.SetBool (m_attackParam, true);
			moverBehaviour.updateInput (Vector3.zero);
			m_animator.SetFloat(m_runSpeedParam, 0);

		} else {
			float normalizedRunSpeed = Math.Abs(verticalLeftStick) + Math.Abs(horizontalLeftStick);
			//float normalizedRunSpeed = velocity.normalized.x;
			m_animator.SetFloat(m_runSpeedParam, normalizedRunSpeed);
			moverBehaviour.updateInput (new Vector3 (verticalLeftStick, 0, horizontalLeftStick));
		}

	}

}
