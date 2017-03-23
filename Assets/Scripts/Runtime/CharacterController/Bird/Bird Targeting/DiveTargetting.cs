using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;

public class DiveTargetting : SelectableAbility {

	//The mover behaviour that takes care of actually making the bird dive
	public DiveAnimation diveAnim;
	public Cinemachine.CinemachineVirtualCamera diveCam;

	private bool diving = false;
	private bool initialized = false;

	[Range(0,100)]
	public float heightOffset = 0;

	public Capturer captureModule;

    private DebugEntities debug;

	protected override void OnEnableTargeting()
	{
		if (!initialized) {
			diveAnim.onDiveComplete += disableAnimCamera;
			initialized = true;
		}

		GameObject debugObject = GameObject.FindGameObjectWithTag("Debug");
		if (debugObject)
		{
			debug = debugObject.GetComponent<DebugEntities>();
		}
	}

	protected override void OnActivate ()
	{
		
		RaycastHit target;
		if (findTarget (out target)) {
			if (!captureModule.containing) {
				if (diveAnim != null && !diving) {
					diveAnim.initialize (this.transform, target.point.y + heightOffset);
					diving = true;
                    if(debug && !debug.m_useWorldCam)
					    diveCam.gameObject.SetActive (true);
				}
			} else {
				captureModule.Release ();
			}

		}
	}

	protected override void OnDisactivate ()
	{
		//Nothing to do
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
