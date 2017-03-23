using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine.Attributes;
using Cinemachine.Blending;
using Cinemachine.Assets;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public abstract class CinemachineController : MonoBehaviour {
	protected CinemachineVirtualCamera controlledView;

	void Start(){
		setCamera ();
		init ();
	}

	public abstract void init ();

	public abstract void increaseYaw (float amt);

	public abstract void increasePitch (float amt);

	public abstract void resetPitchYaw ();

	public abstract void UpdatePosition ();


	protected void setCamera(){
		if(!controlledView)
			controlledView = this.gameObject.GetComponent<CinemachineVirtualCamera> ();
	}

	public void disableCamera(){
		setCamera ();
		controlledView.gameObject.SetActive (false);
	}

	public void enableCamera(){
		setCamera ();
		controlledView.gameObject.SetActive (true);
	}
}
