using UnityEngine;
using System;
using System.Collections;
using Cinemachine.Utility;
using Cinemachine.Attributes;
using Cinemachine.Blending;
using Cinemachine.Assets;
using Cinemachine;
using System.Collections.Generic;

public class BearController : MonoBehaviour {
		
	[Header("Cameras")]
	public Cinemachine3rdPerson runningCam;
	public Cinemachine3rdPerson aimCam;
	public CameraController m_camControls;


	[Space(5)]
	[Header("Character movement")]
	public InputVelocityMoverBehaviour moverBehaviour;

	[Space(5)]
	[Header("Aiming")]
	public GameObject aimingModule;


	[Space(5)]
	[Header("Animation states")]
	public string idleStateName;
	private CharacterControllerStateTrigger idleState;
	public string attackStateName;
	private CharacterControllerStateTrigger attackState;
	public string aimReadyStateName;
	private CharacterControllerStateTrigger aimReadyState;
	public string sleepStateName;
	private CharacterControllerStateTrigger sleepState;
	public string fireStateName;
	private CharacterControllerStateTrigger fireState;

	[Space(5)]
	[Header("AnimationController")]
	public BearAnimationController m_animC;

	//States to know when we can do things
	public bool isIdle = false;
	public bool isAsleep = false;
	public bool isAttacking = false;
	public bool isAiming = false;
	public bool isFiring = false;

	Action enable(bool val){
		return () => val = true;
	}

	Action disable(bool val){
		return () => val = true;
	}

	void Start () {

		if (m_animC.getControllerTrigger (idleStateName, out idleState))
		{
			Debug.Log ("gotIdle");
			idleState.onEnter += enable (isIdle);
			idleState.onExit += disable (isIdle);
		}

		if (m_animC.getControllerTrigger (attackStateName, out attackState))
		{
			attackState.onEnter += enable (isAttacking);
			attackState.onExit += disable (isAttacking);
		}

		if (m_animC.getControllerTrigger (aimReadyStateName, out aimReadyState))
		{
			aimReadyState.onEnter += enable (isAiming);
			aimReadyState.onExit += disable (isAiming);
		}

		if (m_animC.getControllerTrigger (fireStateName, out fireState))
		{
			fireState.onEnter += enable (isFiring);
			fireState.onExit += disable (isFiring);
		}
	}
		
	public void moveBear(Vector3 velocity){
		if (isIdle)
		{
			moverBehaviour.updateInput (velocity);
		} else
		{
			moverBehaviour.updateInput (Vector3.zero);
		}
	}

	public void attack(){
		if ((isAsleep || isIdle) && !isAttacking)
		{
			moverBehaviour.updateInput (Vector3.zero);
			m_animC.doAttack ();
		}
	}

	public void aim(){
		if (isIdle && !isAiming)
		{
			moverBehaviour.updateInput (Vector3.zero);
			m_camControls.changeCamera (aimCam);
			m_animC.startAim ();
		}
	}

	public void disableAim(){
		if (isAiming && !isIdle)
		{
			moverBehaviour.updateInput (Vector3.zero);
			m_camControls.changeCamera (runningCam);
			m_animC.endAim ();
		}
	}


//	void Update () {
//
//		if (isIdle)
//		{
//			currentState = states.Idle;
//		}
//
//		//Bear paw swipe
//		if (Input.GetButtonDown ("Fire1") && (currentState == states.Idle || currentState == states.Sleep))
//		{
//			currentState = states.Attacking;
//			m_animator.SetBool ("idle", false);
//		} else if (Input.GetButtonDown ("Jump") && (currentState == states.Idle))
//		{
//			aim ();
//			currentState = states.Aiming;
//		} 
//		else if (!Input.GetButton ("Jump") && (currentState == states.Aiming))
//		{
//			
//			currentState = states.Idle;
//		} 
//		else if (currentState == states.Idle && normalizedRunSpeed > 0)
//		{ 
//			currentState = states.Running;
//		}
//
//			
//		switch (currentState)
//		{
//		case states.Aiming:
//			changeActiveCam (aimCam);
//			run (Vector3.zero, 0);
//			break;
//		case states.Attacking:
//			run (Vector3.zero, 0);
//			m_animator.SetBool (m_attackParam, true);
//			break;
//		case states.Running:
//			run(new Vector3 (verticalLeftStick, 0, horizontalLeftStick),normalizedRunSpeed);
//			break;
//		case states.Sleep:
//			break;
//		case states.Idle:
//			m_animator.SetBool (m_aimParam, false);
//			m_animator.SetBool (m_attackParam, false);
//			run (Vector3.zero, 0);
//			break;
//		}
//			
//	}
//
//	public void moveBear(Vector3 velocity){
//		
//	}
//
//	private void aim(){
//		//Look in direction of camera, fam :/
//		changeActiveCam (aimCam);
//		m_animator.SetBool (m_aimParam, true);
//		m_animator.SetBool ("idle", false);
//	}
//		
//	
//	private void run(Vector3 moveSpeed, float normalizedRunSpeed){
//		changeActiveCam (runningCam);
//
//		//Update animation
//		m_animator.SetFloat (m_runSpeedParam, normalizedRunSpeed);
//		//Update moverbehaviour
//		moverBehaviour.updateInput (moveSpeed);
//
//		moverBehaviour.enabled = moveSpeed.magnitude>0.0;
//
//	}
//
//	private void changeActiveCam(Cinemachine3rdPerson newCam){
//		Debug.Log (newCam.gameObject.name);
//		if (activeCamera == newCam)
//		{
//			return;
//		}
//
//		if (activeCamera != null)
//		{
//			activeCamera.gameObject.SetActive (false);
//		}
//		activeCamera = newCam;
//		activeCamera.gameObject.SetActive (true);
//	}
}
