using UnityEngine;
using System;
using System.Collections;

using MOJ.Helpers;

/// <summary>
/// The abstract base class for movement behavior.
/// </summary>
[Serializable]
public abstract class MoverBehavior : MonoBehaviour //ScriptableObject
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////  


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////   

	//protected bool m_canInterpolate = true;
	[HideInInspector]
	[SerializeField]
	private bool m_enableUserInput = false;
	[HideInInspector]
	[SerializeField]
	private InterpolationHelper.Vector3Interpolator m_interpolator;
	[HideInInspector]
	[SerializeField]
	private Vector3 m_targetPositionOffset = Vector3.zero;
	[HideInInspector]
	[SerializeField]
	private bool m_moveRelative = true;

	private float m_interpolationTime = 0.0f;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public bool getEnableUserInput() { return m_enableUserInput; }
	public void setEnableUserInput(bool enableUserInput) { m_enableUserInput = enableUserInput; }

	public bool getMoveRelative() { return m_moveRelative; }
	public void setMoveRelative(bool moveRelative) { m_moveRelative = moveRelative; }

	public bool getCanInterpolate()
	{
		return m_interpolator != null;
	}
	//protected void setCanInterpolate(bool canInterpolate) { m_canInterpolate = canInterpolate; }

	public Vector3 getTargetPositionOffset() { return m_targetPositionOffset; }
	public void setTargetPositionOffset(Vector3 offset) { m_targetPositionOffset = offset; }

	public void setInterpolator(InterpolationHelper.Vector3Interpolator interpolator)
	{
		m_interpolator = interpolator;
	}

	public void setInterpolationTime(float time) { m_interpolationTime = time; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Interface Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public virtual void initialize() { }
	public virtual void initialize(MoverBehavior copiedBehavior) { initialize(); }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public virtual void processInputs() { }

	// Return the position that would occur if a move was performed.
	public abstract Vector3 getTargetPosition(Transform transform, float deltaTime);

	//public void resetInterpolationTime()
	//{
	//	m_elapsedTime = 0.0f;
	//}

	// Updates the transform based on the behavior.
	// Returns true if the state of the transform changed.
	public virtual bool tryMove(Transform transform, float deltaTime = 0.0f)
	{
		Vector3 prevPosition = transform.position;
		Vector3 newPosition = prevPosition;								//TODO: FIX THIS TO ALLOW UPDATING OVER THE RIGIDBODY FORCE APPLICATION TIMER UPDATING.

		// If an interpolator was given, use the interpolated position.
        if (m_interpolator != null)
		{
			if(m_interpolationTime < m_interpolator.duration)
			{
				m_interpolationTime = Mathf.Min(m_interpolationTime + deltaTime, m_interpolator.duration);
				newPosition = InterpolationHelper.getInterpolatedVector3(m_interpolator, m_interpolationTime);
			}
		}
		// Otherwise get the final target position.
		else
		{
			newPosition = getTargetPosition(transform, deltaTime);
		}

		//if(m_moveRelative)
		//{
		//	transform.forward = 
		//}
		//else
		//{
			transform.position = newPosition;
		//}
		return true; //!prevPosition.Equals(transform.position);
	}

	//// Updates the transform based on the behavior.
	//// Returns true if the state of the transform changed.
	//public override bool tryMove(Transform transform, float deltaTime)
	//{
	//	transform.position = getTargetPosition(transform, deltaTime);
	//	return true;
	//}

	//// Updates the transform based on the behavior.
	//// Returns true if the state of the transform changed.
	//public virtual bool tryMove(Transform transform, float deltaTime)
	//{
	//	bool stateChanged = false;
	//	if(m_interpolator != null && m_elapsedTime < m_interpolator.duration)
	//	{
	//		m_elapsedTime = Mathf.Min(m_elapsedTime + deltaTime, m_interpolator.duration);
	//		transform.position = InterpolationHelper.getInterpolatedVector3(m_interpolator, m_elapsedTime);
	//		stateChanged = true;
 //       }
	//	return stateChanged;
	//}

	//public virtual void endMove()
	//{
	//}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}