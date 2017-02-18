using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelectableAbility : MonoBehaviour {

	public GameObject indicator;
	public bool enabled = false;
	public bool active = false;
	public LayerMask affectedLayers;

	void Start(){
		indicator.SetActive (false);
	}

	void Update () {
		if (enabled) {
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
		if(!enabled){
			Debug.Log ("enable air targeting module");
			indicator.SetActive (true);
			enabled = true;
			OnEnableTargeting ();
		}
	}

	//Targeter is turned off (turn off targeting display and disable trigger)
	public void Disable(){
		if (enabled){
			enabled = false;
			OnDisableTargeting ();
            indicator.SetActive(false);
        }
	}


	//Do actual action
	public void Activate(){
		if (enabled && !active)
		{
			active = true;
			OnActivate ();
		}
	}

	public void Disactivate(){
		if (active) 
		{
			OnDisactivate ();
			active = false;
		}
	
	}

	//Methods called when the targeter is turned on, triggered, and disabled
	protected abstract void OnEnableTargeting ();
	protected abstract void OnActivate();
	protected abstract void OnDisactivate();
	protected abstract void OnDisableTargeting();

}
