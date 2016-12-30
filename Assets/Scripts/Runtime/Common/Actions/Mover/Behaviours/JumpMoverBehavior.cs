using UnityEngine;
using System;
using System.Collections;

using MOJ.Helpers;

/// <summary>
/// A mover behavior for directional movement.
/// </summary>
public class JumpMoverBehavior : MoverBehavior
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

	public JumpMoverBehavior()
	{
		initialize();
    }

	public JumpMoverBehavior(MoverBehavior copiedBehavior)
	{
		initialize(copiedBehavior);
    }

	public override void initialize(MoverBehavior copiedBehavior)
	{
		JumpMoverBehavior copiedJumpBehavior = copiedBehavior as JumpMoverBehavior;
		if (copiedJumpBehavior != null)
		{
			m_data.copy(copiedJumpBehavior.m_data);
		}
		initialize();
	}

	public override void initialize()
	{
		setCanInterpolate(false);
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Interface Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	public override Vector3 getTargetPosition(Transform transform)
	{
		Debug.Assert(transform != null, "Error: Missing transform when attempting to find the next position.");

		Vector3 targetPosition = transform.position;
		if(m_data.enableUserInput)
		{
			targetPosition += processInputs() + getTargetOffset();
		}

		return targetPosition;
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

	// This ignores interpolation.
	public override bool tryMove(Transform transform, float deltaTime)
	{
        transform.position = getTargetPosition(transform) + processInputs();
		return true;
	}

	private Vector3 processInputs()
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

		Vector3 inputDirection = Vector3.zero;
		TransformHelper.setAxisValue(
			ref inputDirection, 
			TransformHelper.HorizontalAxis, 
			TransformHelper.getAxisValue(horizontalStep, TransformHelper.HorizontalAxis) * horizontalMagnitude
			);
		TransformHelper.setAxisValue(
			ref inputDirection, 
			TransformHelper.VerticalAxis, 
			TransformHelper.getAxisValue(verticalStep, TransformHelper.VerticalAxis) * verticalMagnitude
			);
		return inputDirection;
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