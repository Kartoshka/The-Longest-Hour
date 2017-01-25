using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FadingObjects;

[RequireComponent(typeof(CameraStackManager))]
public class GlobalCameras : TimeBasedObjects {

	public AbCamera playerCamera;
	public AbCamera freeCamera;

	CameraStackManager camStack;

	protected override void initialize ()
	{
	}

	private void switchToFreeCamera()
	{
		this.gameObject.GetComponent<CameraStackManager> ().addGameCam (freeCamera, 0.5f);
	}

	private void switchToPlayer()
	{
		this.gameObject.GetComponent<CameraStackManager> ().addGameCam (playerCamera, 0.5f);
	}


	protected override void OnPause ()
	{
		switchToFreeCamera ();
	}

	protected override void OnResume ()
	{
		switchToPlayer ();
	}

	protected override void PausedUpdate ()
	{
		
	}

	protected override void RunningUpdate ()
	{
		
	}
}
