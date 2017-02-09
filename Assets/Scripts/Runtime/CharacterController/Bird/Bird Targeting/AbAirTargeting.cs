using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbAirTargeting : MonoBehaviour {

	public GameObject indicator;
	public bool active = false;
	public LayerMask affectedLayers;

	void Start(){
		indicator.SetActive (false);
	}

	void Update () {
		if (active) {
			RaycastHit hit;
			if(findTarget(out hit))	{
				indicator.SetActive(true);
				indicator.transform.position = hit.point;
			}else
			{
				indicator.SetActive(false);
			}
		}
	}

	//Shoot a ray down, return whether something was hit, out the information of the hit 
	public bool findTarget(out RaycastHit hit){
		if (Physics.Raycast (transform.position, Vector3.down, out hit, Mathf.Infinity, affectedLayers)) {
            return true;
		} else {
			return false;
		}
	}


	//Targeter is turned on (we can see targeting appearing)
	public void Enable(){
		if(!active){
			Debug.Log ("enable air targeting module");
			indicator.SetActive (true);
			active = true;
			OnEnableTargeting ();
		}
	}

	//Targeter is turned off (turn off targeting display and disable trigger)
	public void Disable(){
		if (active){
			active = false;
			OnDisableTargeting ();
		}
	}

	//Do actual action
	public void Trigger(){
		if (active)	{
			OnTriggerTargeting ();
		}
	}

	//Methods called when the targeter is turned on, triggered, and disabled
	protected abstract void OnEnableTargeting ();
	protected abstract void OnTriggerTargeting();
	protected abstract void OnDisableTargeting();

}
