using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingMovementController : MonoBehaviour
{
	public MovingEntity m_movingEntity;
	public AnimInputController m_animInputController;

	private SteeringBehaviours m_steering;
	private EntityVision m_entityVision;

	public void stopMovement()
	{
		if(m_movingEntity)
		{
			m_movingEntity.enabled = false;
        }
	}

	public void startMovement()
	{
		if (m_movingEntity)
		{
			m_movingEntity.enabled = true;
		}
	}

	// Use this for initialization
	void Start ()
	{
		stopMovement();

		m_steering = this.GetComponent<SteeringBehaviours>();
		if (m_steering)
		{
			m_entityVision = m_steering.getEntityVision();
		}
    }

	//float timer = 10.0f;
	// Update is called once per frame
	void Update ()
	{
		if(m_steering && m_movingEntity && m_entityVision)
		{
			if (m_movingEntity.isInTargetRange())
			{
				m_animInputController.doAttack();
				m_steering.setIsWandering(true);
			}

			GameObject enemyObject = m_entityVision.GetClosestEnemy();
			if (m_steering && enemyObject)
			{
				m_steering.setPursuitTarget(true, enemyObject);
            }
		}
	}
}
