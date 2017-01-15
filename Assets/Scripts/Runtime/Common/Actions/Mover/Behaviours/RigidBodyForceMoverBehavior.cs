using UnityEngine;
using System;
using System.Collections;

using MOJ.Helpers;

/// <summary>
/// A mover behavior for jumping movement.
/// Reference: https://forum.unity3d.com/threads/mario-style-jumping.381906/
/// </summary>
[Serializable]
[AddComponentMenu("MOJCustom/Mover/Rigid Body Force Mover Behaviour")]
public class RigidBodyForceMoverBehavior : MoverBehavior
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 

	[SerializeField]
	[HideInInspector]
	private RigidBodyForceMoverBehaviorData m_data = new RigidBodyForceMoverBehaviorData();

	private float m_forceTimeCounter = 0.0f;
	//private bool m_stoppedApplyingForce = true;
	private bool m_forceInputTriggered = false;

	private bool m_simulateJumping = true;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region  Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	public RigidBodyForceMoverBehavior()
	{
		initialize();
    }

	public RigidBodyForceMoverBehavior(MoverBehavior copiedBehavior)
	{
		initialize(copiedBehavior);
    }

	public override void initialize(MoverBehavior copiedBehavior)
	{
		RigidBodyForceMoverBehavior copiedRigidBodyForceBehavior = copiedBehavior as RigidBodyForceMoverBehavior;
		if (copiedRigidBodyForceBehavior != null)
		{
			m_data.copy(copiedRigidBodyForceBehavior.m_data);
		}
		initialize();
	}

	public override void initialize()
	{
		//setCanInterpolate(false);
		base.initialize();
		m_forceTimeCounter = 0;
		m_forceInputTriggered = false;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////  

	public Vector3 getForceMagnitude() { return m_data.forceMagnitude; }
	public void setForceMagnitude(Vector3 forceMagnitude) { m_data.forceMagnitude = forceMagnitude; }

	public float getDuration() { return m_data.duration; }
	public void setDuration(float duration) { m_data.duration = duration; }

	public Rigidbody getRigidBody() { return m_data.rigidBody; }
	public void setRigidBody(Rigidbody rigidBody) { m_data.rigidBody = rigidBody; }

	public Transform getSurfaceCheckSourceTransform() { return m_data.surfaceCheckSource; }
	public void setSurfaceCheckSourceTransform(Transform transform) { m_data.surfaceCheckSource = transform; }

	public float getSurfaceCheckRadius() { return m_data.surfaceCheckRadius; }
	public void setSurfaceCheckRadius(float surfaceCheckRadius) { m_data.surfaceCheckRadius = surfaceCheckRadius; }

	public LayerMask getSurfaceLayerMask() { return m_data.surfaceLayer; }
	public void setSurfaceLayerMask(LayerMask layerMask) { m_data.surfaceLayer = layerMask; }

	public bool isSimulatingJumping() { return m_simulateJumping; }
	public void setSimulateJumping(bool isSimulatingJumping) { m_simulateJumping = isSimulatingJumping; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	// Return the position that would occur if a move was performed.
	public override Vector3 getTargetPosition(Transform transform, float deltaTime = 0.0f)
	{
		Debug.Assert(transform != null, "Error: Missing transform when attempting to find the next position.");

		simulate();
		return m_data.rigidBody.transform.position;
	}

	//// Updates the transform based on the behavior.
	//// Returns true if the state of the transform changed.
	//// This ignores interpolation.
	//public override bool tryMove(Transform transform, float deltaTime)
	//{
	//	transform.position = getTargetPosition(transform);
	//	return true;
	//}

	private bool shouldApplyForce()
	{
		return m_forceInputTriggered;
	}

	private bool canApplyForce()
	{
		return Physics.CheckSphere(m_data.surfaceCheckSource.position, m_data.surfaceCheckRadius, m_data.surfaceLayer);
	}

	private void applyForce()
	{
		if(m_simulateJumping)
		{
            m_data.rigidBody.velocity = new Vector3(m_data.rigidBody.velocity.x, m_data.forceMagnitude.y);
		}
		else
		{
			m_data.rigidBody.velocity = m_data.forceMagnitude;
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////

	public override void processInputs()
	{
		m_forceInputTriggered = Input.GetKeyDown(KeyCode.Space);
	}

	private void simulate()
	{
		if(shouldApplyForce() && canApplyForce())
		{
			m_forceTimeCounter = m_data.duration;
			m_forceInputTriggered = false;
        }

		if (m_forceTimeCounter > 0)
		{
			applyForce();
			m_forceTimeCounter -= Time.deltaTime;
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}