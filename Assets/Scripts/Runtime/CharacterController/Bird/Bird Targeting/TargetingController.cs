using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour {


	public List<AbAirTargeting> targeters;
	private int currentTarget =0;

	void OnEnable(){
		activateCurrent ();
	}

	void OnDisable()
	{
		disableCurrent ();
	}

	private void disableCurrent(){
		for(int i=0;i<targeters.Count;i++){
			if (i == currentTarget && targeters[i].active) {
				targeters [i].Disable ();
			} 
		}
	}

	private void activateCurrent(){
		for(int i=0;i<targeters.Count;i++){
			if (i == currentTarget && !targeters[i].active) {
				targeters [i].Enable ();
			} else if(targeters[i].active){
				targeters [i].Disable ();
			}
		}
	}

	public void toggle(){
		currentTarget = (currentTarget + 1) % targeters.Count;
		activateCurrent ();
	}

	public void TriggerCurrent(){
		if (targeters [currentTarget] != null) {
			targeters [currentTarget].Trigger ();
		}
	}


}
