using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingController : MonoBehaviour {


	public List<SelectableAbility> targeters;
	private int currentTarget =0;

    int m_DIVE = 0;
    int m_DRAW = 1;
    int m_BEACON = 2;

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

	//public void toggle(){
	//	disableCurrent ();
	//	currentTarget = (currentTarget + 1) % targeters.Count;
	//	enableCurrent ();
	//}

    public void toggleDraw()
    {
        disableCurrent();
        currentTarget = m_DRAW;
        enableCurrent();
    }

    public void toggleDive()
    {
        disableCurrent();
        currentTarget = m_DIVE;
        enableCurrent();
    }

    //public void toggleBeacon()
    //{
    //    disableCurrent();
    //    currentTarget = m_BEACON;
    //    enableCurrent();
    //}

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
