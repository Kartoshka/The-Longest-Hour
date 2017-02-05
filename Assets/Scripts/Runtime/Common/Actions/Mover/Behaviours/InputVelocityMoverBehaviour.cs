using System.Collections;
using System;
using MOJ.Helpers;
using UnityEngine;

[Serializable]
[AddComponentMenu("MOJCustom/Mover/Input Velocity Mover Behaviour")]
public class InputVelocityMoverBehaviour : MoverBehavior {


	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion


	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 	
	public bool relativeToCamera = true;

	//For a given input [0,1], for each axis it is multiplied by this amount
	public Vector3 stepPerAxis = new Vector3(5,5,5);

	public float maxSpeed = 50;

	public Vector3 m_velocity = Vector3.zero;
	public Vector3 Velocity{
		get{
			return m_velocity; 
		}
		set{ 
			float speed = maxSpeed < value.magnitude ? maxSpeed : value.magnitude;
			m_velocity = value.normalized * speed;
		}
	}

	//Normalized velocity
	public Vector3 Heading{
		get{
			return m_velocity.normalized; 
		}
	}
		
	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region  Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion


	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion

	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////


	public override void processInputs ()
	{
		//Nothing I guess?
	}

	public override Vector3 getTargetPosition (Transform transform, float deltaTime)
	{
		return transform.position + m_velocity*deltaTime;
	}


	public void updateInput(Vector3 m_input){

		Vector3 forwardBasis = new Vector3 (1, 0,0);
		Vector3 sideBasis = new Vector3 (0, 0, 1);

		if (relativeToCamera) {
			Vector3 forward = Camera.main.transform.forward;
			forwardBasis = new Vector3 (forward.x,0, forward.z).normalized * stepPerAxis.x;
			sideBasis = new Vector3 (forward.z, 0,-forward.x).normalized * stepPerAxis.z;
		}

		Vector3 heightBasis = Vector3.up.normalized*stepPerAxis.y;


		//Advance forward and sideways based on input
		Velocity = new Vector3 (forwardBasis.x * m_input.x + sideBasis.x * m_input.z + heightBasis.x * m_input.y,
								forwardBasis.y * m_input.x + sideBasis.y * m_input.z + heightBasis.y * m_input.y , 
								forwardBasis.z * m_input.x + sideBasis.z * m_input.z + heightBasis.z * m_input.y);

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
