using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingMovementAnimEvent : MonoBehaviour
{
	public FlockingMovementController m_movementController;

	public void stopMovement()
	{
		if (m_movementController)
		{
			m_movementController.stopMovement();
		}
	}

	public void startMovement()
	{
		if (m_movementController)
		{
			m_movementController.startMovement();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
