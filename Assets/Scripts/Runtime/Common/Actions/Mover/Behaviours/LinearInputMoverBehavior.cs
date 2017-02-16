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
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	//public enum InputDirections
	//{
	//	X,
	//	Y,
	//	Z
	//}

	//[Serializable]
	//private class DirectionInputMap : SerializableDictionary<InputDirections, string> { }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 

	[SerializeField]
	[HideInInspector]
	private LinearInputMoverBehaviorData m_data = new LinearInputMoverBehaviorData();
	//[SerializeField]
	//private DirectionInputMap m_inputMap = new DirectionInputMap();

	private Vector3 m_inputMagnitude = Vector3.zero;

    private DebugEntities debug;

    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////
    #region  Constructors
    //////////////////////////////////////////////////////////////////////////////////////////
    void Start() {
        debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<DebugEntities>();
    }
       
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
		Vector3 targetPosition = transform.position + (Time.deltaTime*calculateTargetPosition(transform)) + getTargetPositionOffset();
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
	private Vector3 calculateTargetPosition(Transform transform)
	{
		float horizontalStep = m_inputMagnitude.x > 0 ? m_data.forwardStepSize.x : m_data.reverseStepSize.x;
		float verticalStep = m_inputMagnitude.y > 0 ? m_data.forwardStepSize.y : m_data.reverseStepSize.y;
		float depthStep = m_inputMagnitude.z > 0 ? m_data.forwardStepSize.z : m_data.reverseStepSize.z;

		Vector3 finalDirection = Vector3.zero;
		if (getMoveRelative())
		{
			// Let x = right, z = forward, y = up. TODO: Perhaps get these axis from the TransformHelper.
			finalDirection += transform.right * horizontalStep * m_inputMagnitude.x;
			finalDirection += transform.up * verticalStep * m_inputMagnitude.y;
			finalDirection += transform.forward * depthStep * m_inputMagnitude.z;
		}
		else
		{
			TransformHelper.setAxisValue(
				ref finalDirection,
				TransformHelper.HorizontalAxis,
				horizontalStep * m_inputMagnitude.x
				);
			TransformHelper.setAxisValue(
				ref finalDirection,
				TransformHelper.VerticalAxis,
				verticalStep * m_inputMagnitude.y
				);
			TransformHelper.setAxisValue(
				ref finalDirection,
				TransformHelper.DepthAxis,
				depthStep * m_inputMagnitude.z
				);
		}
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
            if (!debug.m_useWorldCam)
                m_inputMagnitude.x = Input.GetAxis("Horizontal");
            else
                m_inputMagnitude.x = Input.GetAxis("HorizontalGround");

            m_inputMagnitude.y = 0;

            if(!debug.m_useWorldCam)
			    m_inputMagnitude.z = Input.GetAxis("Vertical");
            else
                m_inputMagnitude.z = Input.GetAxis("VerticalGround");
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