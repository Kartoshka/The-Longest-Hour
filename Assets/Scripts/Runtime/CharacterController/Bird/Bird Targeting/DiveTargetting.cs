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
    
	public Capturer captureModule;

    private DebugEntities debug;

	protected override void OnEnableTargeting()
	{
		if (!initialized) {
			diveAnim.onDiveComplete += disableAnimCamera;
			initialized = true;
		}

        debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<DebugEntities>();
	}

	protected override void OnTriggerTargeting ()
	{
		RaycastHit target;
		if (findTarget (out target)) {
			if (!captureModule.containing) {
				if (diveAnim != null && !diving) {
					diveAnim.initialize (this.transform, target.transform.position.y);
					diving = true;
                    if(!debug.m_useWorldCam)
					    diveCam.gameObject.SetActive (true);
				}
			} else {
				captureModule.Release ();
			}

		}
	}

	protected override void OnDisableTargeting()
	{
		//captureModule.Release ();
	}

	public void disableAnimCamera(){
		diveCam.gameObject.SetActive (false);
		diving = false;
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.GetComponent<Capturable> () != null) {
			captureModule.Capture (c.gameObject.GetComponent<Capturable> ());
		}
	}

}
