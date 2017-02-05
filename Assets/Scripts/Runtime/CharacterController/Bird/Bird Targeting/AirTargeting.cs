using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AirTargeting : MonoBehaviour {

	public GameObject indicator;
	public bool active = false;
	public LayerMask affectedLayers;
	// Update is called once per frame
	void Update () {
		if (active) {
			RaycastHit hit;
			if(findTarget(out hit))	{
				indicator.SetActive (true);
				indicator.transform.position = hit.point;
			}else
			{
				indicator.SetActive(false);
			}
		}
	}

	//Shoot a ray down, return whether something was hit, out the information of the hit 
	public bool findTarget(out RaycastHit hit){
		if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
			return affectedLayers.value == (affectedLayers.value | (1 << hit.collider.gameObject.layer));
		} else {
			return false;
		}
	}

	public abstract bool trigger ();
}
