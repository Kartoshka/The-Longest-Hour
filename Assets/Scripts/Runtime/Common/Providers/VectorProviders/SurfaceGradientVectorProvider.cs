using UnityEngine;
using System;
using MOJ.Helpers;

/// <summary>
/// Provides the directional vector from the closest point on the surface to the point projected from a given source.
/// </summary>
public class SurfaceGradientVectorProvider : VectorProvider
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Atrributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private RaycastData m_raycastData;
	private bool m_isLocalDirection = true;
	private Transform m_localTransform = null;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public RaycastData getRaycastData() { return m_raycastData; }
	public void setRaycastData(RaycastData raycastData) { m_raycastData = raycastData; }

	public bool getIsLocalDirection() { return m_isLocalDirection; }
	public void setIsLocalDirection(bool isLocalDirection) { m_isLocalDirection = isLocalDirection; }

	public Transform getLocalTransform() { return m_localTransform; }
	public void setLocalTransform(Transform localTransform) { m_localTransform = localTransform; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Interface
	//////////////////////////////////////////////////////////////////////////////////////////

	public override Vector3 getVector()
	{
		Vector3 surfaceGradient = Vector3.zero;
		//if(m_raycastData != null)
		//{
		//	Vector3 raycastDirection;
		//	if(m_isLocalDirection && m_localTransform != null)
		//	{
		//		raycastDirection = TransformHelper.worldToLocalCoordinates(m_raycastData.direction, m_localTransform);
		//		raycastDirection = raycastDirection.normalized;
  //          }
		//	else
		//	{
		//		raycastDirection = m_raycastData.direction;
  //          }

		//	RaycastHit hit;
		//	if (Physics.Raycast(m_raycastData.sourceTransform.position, raycastDirection, out hit, m_raycastData.checkDistance, m_raycastData.surfaceLayerMask))
		//	{
		//		RaycastHit closestSurfacePointHit;
		//		if(Physics.Raycast(m_raycastData.sourceTransform.position, hit.normal, out closestSurfacePointHit, m_raycastData.checkDistance, m_raycastData.surfaceLayerMask))
		//		{
		//			Vector3 hitDirection = hit.point - m_raycastData.sourceTransform.position;
		//			surfaceGradient = hit.point - closestSurfacePointHit.point;
  //              }
		//	}
  //      }
		return surfaceGradient;
	}

	#endregion
}
