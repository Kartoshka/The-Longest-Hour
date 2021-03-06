﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class FollowTarget : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public Transform m_target;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	public float m_positionLerpScale = 1.0f;
	public float m_rotationLerpScale = 1.0f;

	public bool m_faceTarget = true;
	public bool m_matchTargetForward = false;
	public Vector3 m_offset = Vector3.zero;
	public string m_followTag;
	public bool followTaggedObj;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		if (m_matchTargetForward)
		{
			m_faceTarget = false;
		}
		if (followTaggedObj)
		{
			GameObject obj = GameObject.FindGameObjectWithTag (m_followTag);
			if (obj)
			{
				m_target = obj.transform;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (m_target)
		{
			this.transform.position = Vector3.Lerp(this.transform.position, m_target.position, m_positionLerpScale * Time.deltaTime) + m_offset;
		}

		Vector3 viewDirection = Vector3.zero;
        if (m_matchTargetForward)
		{
			viewDirection = Vector3.Lerp(this.transform.forward, m_target.forward, m_rotationLerpScale * Time.deltaTime);
        }
		else if (m_faceTarget)
		{
            if(this.transform && m_target)
            {
                viewDirection = Vector3.Lerp(this.transform.forward, m_target.position - this.transform.position, m_rotationLerpScale * Time.deltaTime);
            }
		}

		if (viewDirection != Vector3.zero)
		{
			this.transform.forward = viewDirection;
		}
	}

	#endregion
}