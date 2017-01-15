﻿using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class AnimInputController : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public Rigidbody m_rigidBody;
	public Animator m_animator;
	public string m_parameterName;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region  Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Interface Methods
	//////////////////////////////////////////////////////////////////////////////////////////

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

	// Update is called once per frame
	void Update()
	{
		if(m_animator != null)
		{
			ProcessInputs();
		}
	}

	private void ProcessInputs()
	{
		float normalizedRunSpeed = Input.GetAxis("Horizontal");
		//float normalizedRunSpeed = velocity.normalized.x;
		m_animator.SetFloat(m_parameterName, normalizedRunSpeed);
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}