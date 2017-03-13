using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingMovementController : MonoBehaviour
{
	public SteeringBehaviours m_steeringBehaviours;

	public void stopMovement()
	{
		if(m_steeringBehaviours)
		{
			m_steeringBehaviours.enabled = false;
        }
	}

	public void startMovement()
	{
		if (m_steeringBehaviours)
		{
			m_steeringBehaviours.enabled = true;
		}
	}

	// Use this for initialization
	void Start ()
	{
		stopMovement();
    }

	//float timer = 10.0f;
	// Update is called once per frame
	void Update ()
	{
		//timer -= Time.deltaTime;
		//if(timer < 0)
		//{
		//	startMovement();
  //      }
	}
}
