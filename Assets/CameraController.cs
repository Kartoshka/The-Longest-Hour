using UnityEngine;
using System;
using System.Collections;
using Cinemachine.Utility;
using Cinemachine.Attributes;
using Cinemachine.Blending;
using Cinemachine.Assets;
using Cinemachine;
public class CameraController : MonoBehaviour {

	public CinemachineController defaultCamera;
	public CinemachineController m_activeCam;

	
	public void Update(){
		if (m_activeCam == null && defaultCamera!=null)
		{
			changeCamera (defaultCamera);
		}

	}

	public void changeCamera(CinemachineController newCam){
		if (!this.enabled)
		{
			return;
		}

		if (m_activeCam != newCam && newCam !=null)
		{
			
			//newCam.resetPitchYaw ();
			newCam.enableCamera();
			if (m_activeCam != null)
			{
				m_activeCam.disableCamera ();
			}
			m_activeCam = newCam;
		}
	}

	public CinemachineController getActive(){
		return m_activeCam;
	}

	public void updateCamera(Vector2 change){
		if (m_activeCam)
		{
			m_activeCam.increasePitch (change.y);
			m_activeCam.increaseYaw (change.x);
			m_activeCam.UpdatePosition ();
		}
	}

	public void resetPos(){
		if (m_activeCam && this.enabled)
		{
			m_activeCam.resetPitchYaw ();
		}
	}
}
