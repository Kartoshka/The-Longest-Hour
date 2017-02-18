using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconDrop : SelectableAbility {

	public GameObject beaconPrefab;
	private GameObject currentBeacon;

	protected override void OnEnableTargeting ()
	{
		
	}

	protected override void OnActivate ()
	{
		RaycastHit result;
		if(findTarget(out result) && beaconPrefab!=null){
			if (currentBeacon != null) {
				Destroy (currentBeacon);
			}
			currentBeacon = (GameObject)Instantiate (beaconPrefab, result.point, Quaternion.identity);
		}
	}

	protected override void OnDisactivate ()
	{
	}

	protected override void OnDisableTargeting ()
	{

	}
}
