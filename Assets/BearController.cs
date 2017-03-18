﻿using UnityEngine;
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
	public Cinemachine3rdPerson runningCam;
	public Cinemachine3rdPerson aimCam;
	public CameraController m_camControls;


	[Space(5)]
	[Header("Character movement")]
	public InputVelocityMoverBehaviour moverBehaviour;

	[Space(5)]
	[Header("Actions")]
	public GrenadeControllerComponent grenadeShooter;
	public TagSingle bearSwipeTag;
	public MatchCameraForward aimForwardMatching;
	


	[Space(5)]
	[Header("Animation states")]
	public string idleStateName;
	private CharacterControllerStateTrigger idleState;
	public string attackStateName;
	private CharacterControllerStateTrigger attackState;
	public string startAimStateName;
	private CharacterControllerStateTrigger aimStartState;
	public string endAimStateName;
	private CharacterControllerStateTrigger aimEndState;
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

		if (m_animC.getControllerTrigger (startAimStateName, out aimStartState))
		{
			aimStartState.onEnter += enableAiming;
		}

		if (m_animC.getControllerTrigger (endAimStateName, out aimEndState))
		{
			aimEndState.onExit += disableAiming;
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
		
	public void moveBear(Vector3 velocity){
		if (isIdle)
		{
			moverBehaviour.updateInput (velocity);
		}
		else
		{
			moverBehaviour.updateInput (Vector3.zero);
		}
	}

	public void doAttack(){
		if ((isAsleep || isIdle) && !isAttacking)
		{
			moverBehaviour.updateInput (Vector3.zero);
			m_animC.doAttack ();
			bearSwipeTag.toggleTagging ();
		}
	}

	public void startAim(){
		if (isIdle && !isAiming)
		{
			if (aimForwardMatching)
			{
				aimForwardMatching.enabled = true;
			}
			moverBehaviour.updateInput (Vector3.zero);
			m_camControls.changeCamera (aimCam);
			m_animC.startAim ();
		}
	}

	public void stopAim(){
		if (isAiming && !isIdle)
		{
			if (aimForwardMatching)
			{
				aimForwardMatching.enabled = false;
			}

			moverBehaviour.updateInput (Vector3.zero);
			m_camControls.changeCamera (runningCam);
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


	/*
	 * All the flag setters for our booleans.  
	 * 
	 */

	public void enableSleeping(){
		if (targetFollower)
		{
//			targetFollower.m_faceTarget = false;
//			targetFollower.m_matchTargetForward = true;
		}
		isAsleep = true;
	}
	public void disableSleeping(){
		if (targetFollower)
		{
//			targetFollower.m_faceTarget = true;
//			targetFollower.m_matchTargetForward = false;
		}
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


	private Vector3 getAimOffsetCharacter(){
		Vector3 camForward = m_camControls.getActive ().transform.forward;
		camForward.y = 0;
		Vector3 playerForward = moverBehaviour.transform.position;
		playerForward.y = 0;
		return (camForward - playerForward).normalized;
	}
}
