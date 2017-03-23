using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineRotateObj : CinemachineController {

	public ModifiableForward m_modFwd;
	public float speed = 10f;

	private float totalAngle = 0;

	public override void init ()
	{
		totalAngle = 0;
	}

	public override void increaseYaw (float amt)
	{
		if (m_modFwd)
		{
			amt = amt * speed;
			totalAngle += amt;
			m_modFwd.rotate (amt);
		}
		//Rotate around y
	}

	public override void increasePitch (float amt)
	{

	}

	public override void resetPitchYaw ()
	{
		increaseYaw (-totalAngle);
	}

	public void matchCamera(){
		if (m_modFwd)
		{
			m_modFwd.faceCamera ();
		}
	}

	public override void UpdatePosition ()
	{

	}

}
