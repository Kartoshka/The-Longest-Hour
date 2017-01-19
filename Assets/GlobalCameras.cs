using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraStackManager))]
public class GlobalCameras : MonoBehaviour {

	public AbCamera playerCamera;
	public AbCamera freeCamera;

	CameraStackManager camStack;

	public void switchToFreeCamera()
	{
		Debug.Log ("free");
		this.gameObject.GetComponent<CameraStackManager> ().addGameCam (freeCamera, 2);
	}

	public void switchToPlayer()
	{
		Debug.Log ("player");
		this.gameObject.GetComponent<CameraStackManager> ().addGameCam (playerCamera, 2);
	}
}
