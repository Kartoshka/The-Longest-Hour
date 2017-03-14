using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringController : MonoBehaviour
{
	public SteeringBehaviours m_steeringBehaviours;
	public MovingEntity m_movingEntity;

	public void stopSteering()
	{
		if(m_movingEntity)
		{
			m_movingEntity.enabled = false;
        }
	}

	public void startSteering()
	{
		if (m_movingEntity)
		{
			m_movingEntity.enabled = true;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
