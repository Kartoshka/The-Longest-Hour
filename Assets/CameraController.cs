using UnityEngine;
using System;
using System.Collections;
using Cinemachine.Utility;
using Cinemachine.Attributes;
using Cinemachine.Blending;
using Cinemachine.Assets;
using Cinemachine;
public class CameraController : MonoBehaviour {

	public Cinemachine3rdPerson defaultCamera;
	public Cinemachine3rdPerson activeCamera;

	
	public void Update(){
		if (activeCamera == null && defaultCamera!=null)
		{
			changeCamera (defaultCamera);
		}

	}

	public void changeCamera(Cinemachine3rdPerson newCam){
		
		if (!this.enabled)
		{
			return;
		}

		if (activeCamera != newCam && newCam !=null)
		{
			if (activeCamera != null)
			{
				activeCamera.gameObject.SetActive (false);
			}
			activeCamera = newCam;
			newCam.gameObject.SetActive (true);

		}
	}

	public Cinemachine3rdPerson getActive(){
		return activeCamera;
	}
}
