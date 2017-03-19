using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsParameterController : MonoBehaviour {

	public List<PhysicsTimeCtrl> timeControllables;
	// Use this for initialization
	void Start () {
		
	}
	
	public void register(PhysicsTimeCtrl t){
		if(timeControllables==null){
			timeControllables = new List<PhysicsTimeCtrl>();
		}
		timeControllables.Add (t);
	}

	public void setTime(float t){
		t = Mathf.Clamp (t,-1.0f,1.0f);
		foreach (PhysicsTimeCtrl ctrl in timeControllables)
		{
			if (ctrl.isModifiable)
			{
				ctrl.setTimeScale (t);
			}
		}
	}
}
