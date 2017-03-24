using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineLocatorCam : CinemachineController {

	public MoverComponent localPlayer;
	public MoverComponent target;
	public bool findBear;
	public override void init ()
	{
		GameObject[] players =GameObject.FindGameObjectsWithTag ("LocateTrack");
		foreach (GameObject p in players)
		{
			MoverComponent cmpareTo = p.GetComponent<MoverComponent> ();
				if (cmpareTo && (cmpareTo!=localPlayer))
			{
				target = cmpareTo;
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
