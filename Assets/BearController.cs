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
		
	//TODO add objects which enable on state start, those objects will have scripts that enable other objects. a la component method!
	//fixes bug where bear is looking at floor during sleep
	public FollowTarget targetFollower;

	[Header("Cameras")]
	public CameraController m_camControlers;
	public CinemachineController runningCam;
	public CinemachineController aimCam;
	public CinemachineController locateBirdCam;


	[Space(5)]
	[Header("Character movement")]
	public InputVelocityMoverBehaviour moverBehaviour;

	[Space(5)]
	[Header("Actions")]
	public GrenadeControllerComponent grenadeShooter;
	public TagSingle bearSwipeTag;
	public MatchCameraForward aimForwardMatching;
	public TimeShiftController timeShifter;
	


	[Space(5)]
	[Header("Animation states")]
	public string idleStateName;
	private CharacterControllerStateTrigger idleState;
	public string attackStateName;
	private CharacterControllerStateTrigger attackState;
	public string aimStateName;
	private CharacterControllerStateTrigger aimState;
	public string sleepStateName;
	private CharacterControllerStateTrigger sleepState;
	public string fireStateName;
	private CharacterControllerStateTrigger fireState;

	[Space(5)]
	[Header("AnimationController")]
	public BearAnimationController m_animC;

	//States to know when we can do things
	private bool isIdle = false;
	private bool isAsleep = true;
	private bool isAttacking = false;
	private bool isAiming = false;
	private bool isFiring = false;

	Action enable(bool val){
		return () => val = true;
	}

	Action disable(bool val){
		return () => val = true;
	}

	void Start () {
		
		if (m_animC.getControllerTrigger (idleStateName, out idleState))
		{
			idleState.onEnter += enableIdling;
			idleState.onExit += disableIdling;
		}

		if (m_animC.getControllerTrigger (attackStateName, out attackState))
		{
			attackState.onEnter += enableAttacking;
			attackState.onExit += disableAttacking;
		}

		if (m_animC.getControllerTrigger (aimStateName, out aimState))
		{
			aimState.onEnter += enableAiming;
			aimState.onExit += disableAiming;
		}

		if (m_animC.getControllerTrigger (fireStateName, out fireState))
		{
			fireState.onEnter += enableFiring;
			fireState.onExit += disableFiring;
		}

		if (m_animC.getControllerTrigger (sleepStateName, out sleepState))
		{
			sleepState.onEnter += enableSleeping;
			sleepState.onExit += disableSleeping;
		}
	}
		
	public void moveBear(Vector3 velocity, bool run){
		if (isIdle)
		{
			moverBehaviour.sprinting = run;
			moverBehaviour.updateInput (velocity);
		}
		else
		{
			moverBehaviour.sprinting = false;
			moverBehaviour.updateInput (Vector3.zero);
		}
	}

	public void doAttack(){
		if ((isAsleep || isIdle) && !isAttacking)
		{
			moverBehaviour.updateInput (Vector3.zero);
			m_animC.doAttack ();
		}
	}

	public void startAim(){
		if (isIdle && !isAiming)
		{
//			if (aimForwardMatching)
//			{
//				aimForwardMatching.enabled = true;
//			}
			moverBehaviour.updateInput (Vector3.zero);
			if (aimCam is CinemachineRotateObj)
			{
				((CinemachineRotateObj)aimCam).matchCamera ();
			}
			m_camControlers.changeCamera (aimCam);

			//aimCam.gameObject.SetActive (true);
			m_animC.startAim ();
		}
//		} else if(!isAiming)
//		{
//			m_camControlers.changeCamera (runningCam);
//		}
	}

	public void stopAim(){
		if (isAiming && !isIdle)
		{
			if (aimForwardMatching)
			{
				aimForwardMatching.enabled = false;
			}

			moverBehaviour.updateInput (Vector3.zero);
			//runningCam.resetPitchYaw ();
			m_camControlers.changeCamera (runningCam);

			//aimCam.gameObject.SetActive (false);
			m_animC.endAim ();
		}
	}

	public void fireGrenade(){
		if (isAiming && !isFiring)
		{
			m_animC.shootGrenade ();
			grenadeShooter.FireProjectile ();
		}
	}

//	public GameObject birdTracker;
//	public GameObject bearTracker;
	private bool isLocating = false;
	public void locateOther(bool active){
		
//		if (((isIdle || isAsleep || isAttacking) && active))
//		{
//			runningCam.controlledView.CameraComposerTarget = birdTracker.transform;
//		} else
//		{
//			runningCam.controlledView.CameraComposerTarget = bearTracker.transform;
//		}
		
		if ((isIdle) && active)
		{
			isLocating = true;
			m_camControlers.changeCamera (locateBirdCam);
		} else if(!isAiming)
		{
			if (!active && isLocating)
			{
				isLocating = false;
				m_camControlers.changeCamera (runningCam);
			}
		}

		//locateBirdCam.gameObject.SetActive ();
	}
//
//	public void updateCamera(Vector2 change){
//		CinemachineController active =null;
//		if (isIdle || isAttacking || isAsleep)
//		{
//			active = runningCam;
//		} else if (isAiming)
//		{
//			active = aimCam;
//		}
//		if (active)
//		{
//			active.increasePitch (change.y);
//			active.increaseYaw (change.x);
//			active.UpdatePosition ();
//		}
//	}

	public void setTime(float t){
		t= Mathf.Clamp (t,-1.0f,1.0f);
		if (timeShifter)
		{
			timeShifter.setTime (t);
		}

	}
	public void rewind(float t){
		t= Mathf.Clamp01 (t);

		if (timeShifter)
		{
			timeShifter.setTime (-1 * t);
		}
	}

	public void forward(float t){
		t= Mathf.Clamp01 (t);

		if (timeShifter)
		{
			timeShifter.setTime (t);
		}
	}

	/*
	 * All the flag setters for our booleans.  
	 * 
	 */

	public void enableSleeping(){
		isAsleep = true;
	}
	public void disableSleeping(){
		isAsleep = false;
	}

	public void enableIdling(){
		isIdle = true;
	}
	public void disableIdling(){
		isIdle = false;
	}

	public void enableAttacking(){
		isAttacking = true;
	}
	public void disableAttacking(){
		isAttacking = false;
	}

	public void enableAiming(){
		isAiming = true;
	}
	public void disableAiming(){
		isAiming = false;
	}

	public void enableFiring(){
		isFiring = true;
	}
	public void disableFiring(){
		isFiring = false;
	}



}
