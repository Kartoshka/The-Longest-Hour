using UnityEngine;
using System;
using System.Collections;

using MOJ.Helpers;

/// <summary>
/// A mover behavior for directional movement.
/// </summary>
[Serializable]
[AddComponentMenu("MOJCustom/Mover/Linear Input Mover Behaviour")]
public class LinearInputMoverBehavior : MoverBehavior
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 

	[SerializeField]
	[HideInInspector]
	private LinearInputMoverBehaviorData m_data = new LinearInputMoverBehaviorData();

	private Vector3 m_inputMagnitude = Vector3.zero;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region  Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	public LinearInputMoverBehavior()
	{
		initialize();
    }

	public LinearInputMoverBehavior(MoverBehavior copiedBehavior)
	{
		initialize(copiedBehavior);
    }

	public override void initialize(MoverBehavior copiedBehavior)
	{
		LinearInputMoverBehavior copiedLinearInputBehavior = copiedBehavior as LinearInputMoverBehavior;
		if (copiedLinearInputBehavior != null)
		{
			m_data.copy(copiedLinearInputBehavior.m_data);
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

	public Vector3 getForwardStepSize() { return m_data.forwardStepSize; }
	public void setForwardStepSize(Vector3 forwardStepSize) { m_data.forwardStepSize = forwardStepSize; }

	public Vector3 getReverseStepSize() { return m_data.reverseStepSize; }
	public void setReverseStepSize(Vector3 reverseStepSize) { m_data.reverseStepSize = reverseStepSize; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	// Return the position that would occur if a move was performed.
	public override Vector3 getTargetPosition(Transform transform, float deltaTime = 0.0f)
	{
		Debug.Assert(transform != null, "Error: Missing transform when attempting to find the next position.");
		Vector3 targetPosition = transform.position + calculateTargetPosition() + getTargetPositionOffset();
		return targetPosition;
	}

	//// Updates the transform based on the behavior.
	//// Returns true if the state of the transform changed.
	//// This ignores interpolation.
	//public override bool tryMove(Transform transform, float deltaTime)
	//{
 //       transform.position = getTargetPosition(transform);
	//	return true;
	//}

	// Calculate the direction of the movement behavior after processing user inputs.
	private Vector3 calculateTargetPosition()
	{
		Vector3 horizontalStep = m_inputMagnitude.x > 0 ? m_data.forwardStepSize : m_data.reverseStepSize;
		Vector3 verticalStep = m_inputMagnitude.y > 0 ? m_data.forwardStepSize : m_data.reverseStepSize;
		//Vector3 depthStep;

		Vector3 finalDirection = Vector3.zero;
		TransformHelper.setAxisValue(
			ref finalDirection,
			TransformHelper.HorizontalAxis,
			TransformHelper.getAxisValue(horizontalStep, TransformHelper.HorizontalAxis) * m_inputMagnitude.x
			);
		TransformHelper.setAxisValue(
			ref finalDirection,
			TransformHelper.VerticalAxis,
			TransformHelper.getAxisValue(verticalStep, TransformHelper.VerticalAxis) * m_inputMagnitude.y
			);
		//TransformHelper.setAxisValue(
		//	ref finalDirection,
		//	TransformHelper.DepthAxis,
		//	TransformHelper.getAxisValue(depthStep, TransformHelper.DepthAxis) * m_inputMagnitude.z
		//	);
		return finalDirection;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////

	public override void processInputs()
	{
		if(getEnableUserInput())
		{
			m_inputMagnitude.x = Input.GetAxis("Horizontal");
			m_inputMagnitude.y = Input.GetAxis("Vertical");
		}
		else
		{
			m_inputMagnitude = Vector3.one;
		}
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}