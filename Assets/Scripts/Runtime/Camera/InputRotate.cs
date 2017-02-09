using UnityEngine;
using System.Collections;

public class InputRotate : MonoBehaviour
{
	public Transform m_target;

	public float m_rate = 2.0f;
	public Vector2 m_angleLimits = new Vector2(-45, 45);
	private float m_currentAngle = 0.0f;

	// Update is called once per frame
	void Update ()
	{
		//float yawInput = Input.GetAxis("HorizontalRightStick");
		float pitchInput = Input.GetAxis("VerticalRightStick");

		if (pitchInput != 0)
		{
			m_currentAngle += m_rate * pitchInput;
			m_target.localEulerAngles = new Vector3(Mathf.Clamp(m_currentAngle, m_angleLimits.x, m_angleLimits.y), m_target.localEulerAngles.y, m_target.localEulerAngles.z);
			//float pitch = m_target.localEulerAngles.x + m_rate * pitchInput;
			//if(pitch > 180)
			//{
			//	pitch 
			//}
			//m_target.localEulerAngles = new Vector3(Mathf.Clamp(pitch, m_angleLimits.x, m_angleLimits.y), m_target.localEulerAngles.y, m_target.localEulerAngles.z);
		}
	}
}
