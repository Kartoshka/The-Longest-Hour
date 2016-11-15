﻿using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("")] // Hidden within inspector.
public class Observer<T>
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	////////////////////////////////////////////////////////////////////////////////////////// 

	public delegate void Listener(T owner);

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private List<Listener> m_listeners = new List<Listener>();
	private T m_owner;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	public Observer(T owner)
	{
		m_owner = owner;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	public void add(Listener listener)
	{
		m_listeners.Add(listener);
	}

	public void remove(Listener listener)
	{
		m_listeners.Remove(listener);
	}

	public void notify()
	{
		// Avoid issues with adding and removing listeners during the loop by iterating backwards.
		for (int i = m_listeners.Count - 1; i >= 0; --i)
		{
			m_listeners[i](m_owner);
		}
	}

	public void clear()
	{
		m_listeners.Clear();
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
