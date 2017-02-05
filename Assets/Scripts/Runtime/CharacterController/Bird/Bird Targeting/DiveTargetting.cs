using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveTargetting : AirTargeting {

	public DiveAnimation diveAnim;
    public override bool trigger ()
	{
		RaycastHit target;
		if (findTarget (out target)) {
			if (diveAnim!=null && !diveAnim.active) {
				diveAnim.initialize (this.transform, target.transform.position.y);
			}
			return true;
		}
		return false;
	}
}
