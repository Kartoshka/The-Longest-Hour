﻿using UnityEngine;
using System.Collections.Generic;


public class CameraManager : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes

	private struct CameraStackData{
		//Camera data
		public AbCamera cam;
		//0.0 - 1.0
		public float desiredBlend;
		//How much to blend in per frame, calculated based on blend duration and start blending
		public float blendIncrement;
	}

	private LinkedList<CameraStackData> gameCamStack;


	public Camera mainCam;

	public AbCamera baseGameplayCamera;
	//////////////////////////////////////////////////////////////////////////////////////////


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	public Vector3 getCurrentViewPosition(){
		return this.mainCam.transform.position;
	}

	public Quaternion getCurrentViewRotation(){
		return this.mainCam.transform.rotation;
	}

	private void updateGameplayStack()
	{
		Vector3 finalPosition = Vector3.zero;
		Vector3 finalTarget = Vector3.zero;

		float totalBlend = 0;
		int length = gameCamStack.Count;

		for(int i=0;i<length;i++){
			CameraStackData c = gameCamStack.First.Value;

			float apparentBlending = 1.0f - totalBlend;
			//Hacky way to increment blend amount
			if (c.desiredBlend <= 0) {
				break;
			} else if(c.desiredBlend<1.0){
				c.desiredBlend += c.blendIncrement!=1.0f?c.blendIncrement * Time.deltaTime:1.0f;
			}
				
			apparentBlending = apparentBlending > c.desiredBlend ? c.desiredBlend : apparentBlending;
			//Add to vector based on apparent blending amount
			finalPosition += c.cam.getPosition() * apparentBlending;
			finalTarget += c.cam.getTarget() * apparentBlending;

			totalBlend += apparentBlending;

			//Move camera to end of stack
			gameCamStack.RemoveFirst ();
			//If the camera isn't visible, remove it forever from the stack
			if (apparentBlending > 0) {
				gameCamStack.AddLast (c);
			} else {
				Destroy (c.cam.gameObject);
			}
		}

		this.mainCam.transform.position = finalPosition;
		this.mainCam.gameObject.transform.LookAt (finalTarget);
	}

	public void addGameCam(AbCamera cam, float blendInTime)
	{

		CameraStackData c;
		c.cam = cam;
		c.desiredBlend = 0.00000001f;
		c.blendIncrement = blendInTime == 0 ? 1.0f : 1.0f / blendInTime;
		gameCamStack.AddFirst (c);
		
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start ()
	{
		this.gameCamStack = new LinkedList<CameraStackData> ();
		this.baseGameplayCamera = ((GameObject)Instantiate (this.baseGameplayCamera.gameObject)).GetComponent<AbCamera> ();
		this.addGameCam (this.baseGameplayCamera, 0f);
	}

	// Update is called once per frame
	void Update ()
	{
		ProcessInputs();
	}

	private void ProcessInputs()
	{
		updateGameplayStack ();
	}
		
	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}