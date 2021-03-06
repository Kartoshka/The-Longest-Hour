﻿using UnityEngine;
using System;
using System.Collections;

using MOJ.Helpers;

[Serializable]
public class SurfaceMoverBehaviorData
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	////////////////////////////////////////////////////////////////////////////////////////// 

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 

	// Add new variables for loading and saving here.
	//public Vector3 positionOffset = Vector3.zero;
	public Transform surfaceCheckSource = null;
	public Vector3 defaultRayCastDirection = Vector3.zero;
	public LayerMask surfaceLayer;
	public float friction = 1.0f;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region  Constructors
	////////////////////////////////////////////////////////////////////////////////////////// 

	public SurfaceMoverBehaviorData() { }

	public SurfaceMoverBehaviorData(SurfaceMoverBehaviorData data)
	{
		if (data != null)
		{
			copy(data);
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public void copy(SurfaceMoverBehaviorData rhs)
	{
		//positionOffset = rhs.positionOffset;
		surfaceCheckSource = rhs.surfaceCheckSource;
        defaultRayCastDirection = rhs.defaultRayCastDirection;
		surfaceLayer = rhs.surfaceLayer;
	}

	#endregion
}