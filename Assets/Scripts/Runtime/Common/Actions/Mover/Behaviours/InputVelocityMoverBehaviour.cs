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

	public enum Axis
	{
		X,
		Y,
		Z
	}

	#endregion


	//////////////////////////////////////////////////////////////////////////////////////////
	#region Properties
	////////////////////////////////////////////////////////////////////////////////////////// 

	//Camera relative
	public Cinemachine3rdPerson relativeView;


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

	private bool constrainX = false;
	private bool constrainY = false;
	private bool constrainZ = false;

	public Axis constrainAxis;

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
		target = transform;

		return transform.position + m_velocity*deltaTime;
	}
	Vector2 xBasis;
	Vector2 yBasis;
	Transform target;

	public void updateInput(Vector2 m_input){

		 xBasis = new Vector2 (1, 0);
		 yBasis = new Vector2 (0, 1);


			Vector3 forward = Camera.main.transform.forward;
			xBasis = new Vector2 (forward.x, forward.z).normalized*stepPerAxis.x;
			yBasis = new Vector2 (forward.z, -forward.x).normalized*stepPerAxis.y;

		//Velocity = new Vector3 (m_input.x*stepPerAxis.x, 0,m_input.y*stepPerAxis.z);
		Velocity = new Vector3 (xBasis.x * m_input.x + yBasis.x * m_input.y,0, xBasis.y * m_input.x + yBasis.y * m_input.y);

	}

	public void OnDrawGizmos(){
		Gizmos.DrawLine(transform.position,transform.position+new Vector3 (xBasis.x ,0, xBasis.y ));
		Gizmos.DrawLine(transform.position,transform.position+new Vector3 (yBasis.x,0, yBasis.y));
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
