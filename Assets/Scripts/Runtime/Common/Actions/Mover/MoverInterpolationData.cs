using UnityEngine;
using System;
using System.Collections;

using MOJ.Helpers;

[Serializable]
public class MoverInterpolationData
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	////////////////////////////////////////////////////////////////////////////////////////// 

	//public enum MoverBehaviorType
	//{
	//	Undefined = -1,
	//	Linear,
	//}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 

	// Add new variables for loading and saving here.
//	public MoverBehaviorType moverBehaviorType = MoverBehaviorType.Undefined;
	public InterpolationHelper.InterpolationType interpolationType = InterpolationHelper.InterpolationType.None;
	public InterpolationHelper.EasingType easingType = InterpolationHelper.EasingType.None;
	public float updateRate = -1.0f;
	public float duration = 0.01f;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region  Constructors
	////////////////////////////////////////////////////////////////////////////////////////// 

	public MoverInterpolationData() { }

	public MoverInterpolationData(MoverInterpolationData data)
	{
		if(data != null)
		{
			copy(data);
		}
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public void copy(MoverInterpolationData rhs)
    {
//		moverBehaviorType = rhs.moverBehaviorType;
		interpolationType = rhs.interpolationType;
		easingType = rhs.easingType;
		updateRate = rhs.updateRate;
		duration = rhs.duration;
	}

	#endregion
}