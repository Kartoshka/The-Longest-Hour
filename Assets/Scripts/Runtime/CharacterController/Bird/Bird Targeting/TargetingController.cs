using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour {


	public List<SelectableAbility> targeters;
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
			if (i == currentTarget && targeters[i].enabled) {
				targeters [i].Disable ();
			} 
		}
	}

	private void enableCurrent(){
		for(int i=0;i<targeters.Count;i++){
			if (i == currentTarget && !targeters[i].enabled) {
				targeters [i].Enable ();
			} else if(targeters[i].enabled){
				targeters [i].Disable ();
			}
		}
	}

	public void toggle(){
		disableCurrent ();
		currentTarget = (currentTarget + 1) % targeters.Count;
		enableCurrent ();
	}

	public void activateCurrent(){
		if (targeters [currentTarget] != null) {
			targeters [currentTarget].Activate ();
		}
	}

	public void disactivateCurrent(){
		if (targeters [currentTarget] != null) {
			targeters [currentTarget].Disactivate ();
		}
	}

}
