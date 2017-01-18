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

	protected bool m_canInterpolate = true;
	private InterpolationHelper.Vector3Interpolator m_interpolator;
	protected Vector3 m_targetOffset = Vector3.zero;

	protected float m_elapsedTime = 0.0f;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public bool getCanInterpolate() { return m_canInterpolate; }
	protected void setCanInterpolate(bool canInterpolate) { m_canInterpolate = canInterpolate; }

	public Vector3 getTargetOffset() { return m_targetOffset; }
	public void setTargetOffset(Vector3 offset) { m_targetOffset = offset; }

	public void setInterpolator(InterpolationHelper.Vector3Interpolator interpolator)
	{
		m_interpolator = interpolator;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Interface Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public abstract void initialize();
	public abstract void initialize(MoverBehavior copiedBehavior);

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	// Return the position that would occur if a move was performed.
	public virtual Vector3 getTargetPosition(Transform transform) { return transform.position; }

	//public virtual void beginMove()
	//{
	//   }

	public void resetPosition()
	{
		m_elapsedTime = 0.0f;
	}

	// Updates the transform based on the behavior.
	// Returns true if the state of the transform changed.
	public virtual bool tryMove(Transform transform, float deltaTime)
	{
		bool stateChanged = false;
		if(m_interpolator != null && m_elapsedTime < m_interpolator.duration)
		{
			m_elapsedTime = Mathf.Min(m_elapsedTime + deltaTime, m_interpolator.duration);
			transform.position = InterpolationHelper.getInterpolatedVector3(m_interpolator, m_elapsedTime);
			stateChanged = true;
        }
		return stateChanged;
	}

	//public virtual void endMove()
	//{
	//}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}