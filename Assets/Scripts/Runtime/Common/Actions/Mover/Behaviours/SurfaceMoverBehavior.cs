using UnityEngine;
using System;
using System.Collections;

using MOJ.Helpers;

/// <summary>
/// A mover behavior for directional movement.
/// </summary>
[Serializable]
[AddComponentMenu("MOJCustom/Mover/Attach To Surface Mover Behaviour")]
public class SurfaceMoverBehavior : MoverBehavior
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 

	[SerializeField]
	[HideInInspector]
	private SurfaceMoverBehaviorData m_data = new SurfaceMoverBehaviorData();

	private GeometryHelper.HitPoint m_surfacePoint;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region  Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	public SurfaceMoverBehavior()
	{
		initialize();
    }

	public SurfaceMoverBehavior(MoverBehavior copiedBehavior)
	{
		initialize(copiedBehavior);
    }

	public override void initialize(MoverBehavior copiedBehavior)
	{
		SurfaceMoverBehavior copiedSurfaceBehavior = copiedBehavior as SurfaceMoverBehavior;
		if (copiedSurfaceBehavior != null)
		{
//			m_data.copy(copiedSurfaceBehavior.m_data);
		}
		initialize();
	}

	//public override void initialize()
	//{
	//	setCanInterpolate(false);
	//}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////  

	//public Vector3 getPositionOffset() { return m_data.positionOffset; }
	//public void setPositionOffset(Vector3 positionOffset) { m_data.positionOffset = positionOffset; }

	public Transform getSurfaceCheckSourceTransform() { return m_data.surfaceCheckSource; }
	public void setSurfaceCheckSourceTransform(Transform transform) { m_data.surfaceCheckSource = transform; }

	public Vector3 getDefaultRaycastDirection() { return m_data.defaultRayCastDirection; }
	public void setDefaultRaycastDirection(Vector3 rayCastDirection) { m_data.defaultRayCastDirection = rayCastDirection; }

	public LayerMask getSurfaceLayerMask() { return m_data.surfaceLayer; }
	public void setSurfaceLayerMask(LayerMask layerMask) { m_data.surfaceLayer = layerMask; }

	public float getFrictionValue() { return m_data.friction; }
	public void setFrictionValue(float friction) { m_data.friction = friction; }


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public override Vector3 getTargetPosition(Transform transform, float deltaTime = 0.0f)
	{
		Debug.Assert(transform != null, "Error: Missing transform when attempting to find the next position.");

		Vector3 rayCastDirection;
		if (m_surfacePoint != null && m_surfacePoint.normal != Vector3.zero)
		{
			//rayCastDirection = m_surfacePoint.normal * -1;
			rayCastDirection = Vector3.Lerp(m_data.defaultRayCastDirection, m_surfacePoint.normal * -1, m_data.friction);
		}
		else
		{
			rayCastDirection = m_data.defaultRayCastDirection;
		}

		Vector3 targetPosition;
		updateHitPoint(m_data.surfaceCheckSource, rayCastDirection);
		if (m_surfacePoint != null)
		{
			targetPosition = m_surfacePoint.position;
		}
		else
		{
			targetPosition = transform.position;
		}

		//Debug.DrawRay(m_data.surfaceCheckSource.position, rayCastDirection * 50, Color.green);
		//Debug.DrawRay(m_surfacePoint.position, m_surfacePoint.normal * 2, Color.red);
		return targetPosition;
	}

	public override bool tryMove(Transform transform, float deltaTime = 0.0f)
	{
		bool success = base.tryMove(transform);
		// Re-orient to surface and offset
		if(m_surfacePoint != null)
		{
			transform.up = m_surfacePoint.normal;
			transform.Translate(getTargetPositionOffset());
		}
        return success;
	}

	private void updateHitPoint(Transform sourceTransform, Vector3 rayCastDirection)
	{
		m_surfacePoint = null;
		RaycastHit hit;
		//Check for water surface below transform position
		if (Physics.Raycast(sourceTransform.position, rayCastDirection, out hit, 500.0f, m_data.surfaceLayer))
		{
			m_surfacePoint = GeometryHelper.retrieveHitPoint(hit);
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}