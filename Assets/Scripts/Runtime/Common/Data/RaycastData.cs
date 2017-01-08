using UnityEngine;
using System;


public struct RaycastData
{
	public Transform sourceTransform;
	public Vector3 direction;
	public LayerMask surfaceLayerMask;
	public float checkDistance;
}
