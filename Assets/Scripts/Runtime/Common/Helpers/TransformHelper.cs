using UnityEngine;
using System.Collections;

namespace MOJ.Helpers
{

public class TransformHelper
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	////////////////////////////////////////////////////////////////////////////////////////// 

	public enum Axis
	{
		X,
		Y,
		Z
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	////////////////////////////////////////////////////////////////////////////////////////// 

	public static Axis HorizontalAxis = Axis.X;
	public static Axis VerticalAxis = Axis.Y;
	public static Axis DepthAxis = Axis.Z;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public static float getAxisValue(Vector3 inVector, Axis inAxis)
	{
		switch (inAxis)
		{
		case(Axis.X) :
			return inVector.x;
		case(Axis.Y) :
			return inVector.y;
		case(Axis.Z) :
			return inVector.z;
		}
		return 0;
	}

	public static void setAxisValue(ref Vector3 inVector, Axis inAxis, float value)
	{
		switch (inAxis)
		{
			case (Axis.X):
			{
				inVector.x = value;
				break;
			}
			case (Axis.Y):
			{
				inVector.y = value;
				break;
			}
			case (Axis.Z):
			{
				inVector.z = value;
				break;
			}
		}
	}

	public static Vector3 getLocalAxisDirection(Transform transform, Axis inAxis)
	{
		switch (inAxis)
		{
			case (Axis.X):
				return transform.right;
			case (Axis.Y):
				return transform.up;
			case (Axis.Z):
				return transform.forward;
		}
		return Vector3.zero;
	}

	// Given a point relative to a transform, return the point in world coordinates.
	public static Vector3 localToWorldCoordinates(Vector3 localCoordinates, Transform sceneTransform)
	{
		Vector4 homogeneousCoords = new Vector4(localCoordinates.x, localCoordinates.y, localCoordinates.z, 1);
		return sceneTransform.localToWorldMatrix * homogeneousCoords;
	}

	// Given a point in space and a transform, return the point relative to the given transform.
	public static Vector3 worldToLocalCoordinates(Vector3 worldCoordinates, Transform sceneTransform)
	{
		Vector4 homogeneousCoords = new Vector4(worldCoordinates.x, worldCoordinates.y, worldCoordinates.z, 1);
		return sceneTransform.worldToLocalMatrix * homogeneousCoords;
	}

	#endregion
}

}