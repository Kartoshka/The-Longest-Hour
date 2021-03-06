﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionObserver : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private Observer<CollisionObserver> m_observer;

	private Collision m_previousCollision;
	private Collider m_previousCollider;
	private bool m_isEntering = false;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public Observer<CollisionObserver> getObserver()
	{
		if(m_observer == null)
		{
			createObserver();
		}
		return m_observer;
	}

	public Collision getPreviousCollision() { return m_previousCollision; }
	public Collider getPreviousCollider() { return m_previousCollider; }
	public bool getIsEntering() { return m_isEntering; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private void createObserver()
	{
		m_observer = new Observer<CollisionObserver>(this);
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	void Awake()
	{
		createObserver();
	}

	void OnCollisionEnter(Collision collision)
	{
		m_previousCollision = collision;
		m_isEntering = true;
        m_observer.notify();
	}

	void OnTriggerEnter(Collider collider)
	{
		m_previousCollider = collider;
		m_isEntering = true;
        m_observer.notify();
	}

	void OnTriggerExit(Collider collider)
	{
		m_previousCollider = collider;
		m_isEntering = false;
		m_observer.notify();
	}

	#endregion

}
