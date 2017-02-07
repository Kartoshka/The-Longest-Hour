using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;

public class DiveTargetting : AbAirTargeting {

	//The mover behaviour that takes care of actually making the bird dive
	public DiveAnimation diveAnim;
	public Cinemachine.CinemachineVirtualCamera diveCam;

	private bool diving = false;

	private bool initialized = false;

	protected override void OnEnableTargeting()
	{
		if (!initialized) {
			diveAnim.onDiveComplete += disableAnimCamera;
			initialized = true;
		}
	}

	protected override void OnTriggerTargeting ()
	{
		RaycastHit target;
		if (findTarget (out target)) {
			if (diveAnim!=null && !diving) {
				diveAnim.initialize (this.transform, target.transform.position.y);
				diving = true;
				diveCam.gameObject.SetActive (true);
			}
		}
	}

	protected override void OnDisableTargeting()
	{

	}

	public void disableAnimCamera(){
		diveCam.gameObject.SetActive (false);
		diving = false;
	}

}
