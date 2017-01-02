using UnityEngine;
using System;
using System.Collections;

using MOJ.Helpers;

/// <summary>
/// A mover behavior for directional movement.
/// </summary>
[Serializable]
public class LinearInputMoverBehavior : MoverBehavior
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 

	[SerializeField]
	private LinearInputMoverBehaviorData m_data = new LinearInputMoverBehaviorData();

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

	public override void initialize()
	{
		setCanInterpolate(false);
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////  

	public bool getEnableUserInput() { return m_data.enableUserInput; }
	public void setEnableUserInput(bool enableUserInput) { m_data.enableUserInput = enableUserInput; }

	public Vector3 getForwardStepSize() { return m_data.forwardStepSize; }
	public void setForwardStepSize(Vector3 forwardStepSize) { m_data.forwardStepSize = forwardStepSize; }

	public Vector3 getReverseStepSize() { return m_data.reverseStepSize; }
	public void setReverseStepSize(Vector3 reverseStepSize) { m_data.reverseStepSize = reverseStepSize; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	// Return the position that would occur if a move was performed.
	public override Vector3 getTargetPosition(Transform transform)
	{
		Debug.Assert(transform != null, "Error: Missing transform when attempting to find the next position.");
		Vector3 targetPosition = transform.position + calculateTargetPosition() + getTargetOffset();
		return targetPosition;
	}

	// Updates the transform based on the behavior.
	// Returns true if the state of the transform changed.
	// This ignores interpolation.
	public override bool tryMove(Transform transform, float deltaTime)
	{
        transform.position = getTargetPosition(transform);
		return true;
	}

	// Calculate the direction of the movement behavior after processing user inputs.
	private Vector3 calculateTargetPosition()
	{
		float horizontalMagnitude = 1;
		float verticalMagnitude = 1;
		if (m_data.enableUserInput)
		{
			horizontalMagnitude = Input.GetAxis("Horizontal");
			verticalMagnitude = Input.GetAxis("Vertical");
		}

		Vector3 horizontalStep = horizontalMagnitude > 0 ? m_data.forwardStepSize : m_data.reverseStepSize;
		Vector3 verticalStep = verticalMagnitude > 0 ? m_data.forwardStepSize : m_data.reverseStepSize;

		Vector3 finalDirection = Vector3.zero;
		TransformHelper.setAxisValue(
			ref finalDirection,
			TransformHelper.HorizontalAxis,
			TransformHelper.getAxisValue(horizontalStep, TransformHelper.HorizontalAxis) * horizontalMagnitude
			);
		TransformHelper.setAxisValue(
			ref finalDirection,
			TransformHelper.VerticalAxis,
			TransformHelper.getAxisValue(verticalStep, TransformHelper.VerticalAxis) * verticalMagnitude
			);
		return finalDirection;
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