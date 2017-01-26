using UnityEngine;
using System;

[Serializable]
public class RaycastData : MonoBehaviour
{
	public Transform sourceTransform;
	public Vector3 direction;
	public LayerMask surfaceLayerMask;
	public float checkDistance;
}
