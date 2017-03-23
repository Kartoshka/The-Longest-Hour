using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineLocatorCam : CinemachineController {

	public GameObject localPlayer;
	public GameObject target;

	public override void init ()
	{
		GameObject[] players =GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject p in players)
		{
			if (p != localPlayer)
			{
				target = p;
			}
		}
		if (!target)
		{
			target = localPlayer;
		}

		this.controlledView.CameraComposerTarget = target.transform;
	}

	public override void increaseYaw (float amt)
	{
		//throw new System.NotImplementedException ();
	}

	public override void increasePitch (float amt)
	{
		//throw new System.NotImplementedException ();
	}

	public override void resetPitchYaw ()
	{
		//throw new System.NotImplementedException ();
	}

	public override void UpdatePosition ()
	{
		//throw new System.NotImplementedException ();
	}
}
