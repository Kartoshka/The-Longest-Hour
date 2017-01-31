using UnityEngine;
using System.Collections;

using MOJ.Helpers;

/// <summary>
///
/// </summary>
public class TimeShifterAimDirection : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	public Transform m_targetTransform;
	public AnimParameterController m_animCtrl;
	public float m_rate = 1.0f;

	private Vector3 m_previousAimDirection;

	private float m_angle; // TODO: Remove this. It's just for debugging.

	public float m_startDelay = 5.0f;

	private float m_time = 0.0f;
	private float m_delayTimer;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	public void reset()
	{
		//m_time = 0.0f;
		m_previousAimDirection = m_targetTransform.position - this.transform.position;
		m_delayTimer = m_startDelay;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		ProcessInputs();
		if(m_delayTimer > 0.0f)
		{
			m_delayTimer -= Time.deltaTime;
        }
		else if (m_targetTransform)
		{
			Vector3 currentAimDirection = m_targetTransform.position - this.transform.position;
			//float dot = Vector3.Dot(currentAimDirection, m_previousAimDirection);

			Debug.DrawLine(this.transform.position, this.transform.position + currentAimDirection, Color.green);
			Debug.DrawLine(this.transform.position, this.transform.position + m_previousAimDirection, Color.blue);
			Debug.DrawLine(this.transform.position, m_targetTransform.position, Color.red);

			float turnAngle = GeometryHelper.get2DTurnDirection(
                m_previousAimDirection.x, m_previousAimDirection.z
                , this.transform.position.x, this.transform.position.z
				, currentAimDirection.x, currentAimDirection.z
                );

			m_angle = turnAngle; // TODO: Remove this. It's just for debugging.

			m_time += m_rate * turnAngle;
			float angle = Vector3.Angle(this.transform.position + m_previousAimDirection, this.transform.position + currentAimDirection);
			m_angle = angle;
            if (turnAngle > 0)
			{
				m_time += angle * m_rate;
			}
			else
			{
				m_time -= angle * m_rate;
			}

			m_previousAimDirection = currentAimDirection;
		}

		m_time = Mathf.Clamp01(m_time);
		if (m_animCtrl)
		{
			m_animCtrl.setParamValue(m_animCtrl.timeParameter, m_time);
		}
	}

	private void ProcessInputs()
	{

	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}