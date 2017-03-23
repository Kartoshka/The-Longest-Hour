using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventApplyAttack : MonoBehaviour
{
	public MovingEntity m_movingEntity;

	public void attackStart()
	{
		if(m_movingEntity)
		{
			if(m_movingEntity.isInTargetRange())
			{
				GameObject targetObject = m_movingEntity.getTargetObject();
				if(targetObject)
				{
					TakeDamage takeDamageComponent = targetObject.GetComponent<TakeDamage>();
					if(takeDamageComponent)
					{
						takeDamageComponent.applyDamage();
					}
				}
			}
		}
	}

	public void attackEnd()
	{

	}

	//// Use this for initialization
	//void Start()
	//{
	//	if (m_collisionObserver)
	//	{
	//		m_collisionListener = createCollisionListener();
	//		Observer<CollisionObserver> observer = m_collisionObserver.getObserver();
	//		observer.add(m_collisionListener);
	//	}
	//}


	//private Observer<CollisionObserver>.Listener createCollisionListener()
	//{
	//	m_collisionListener = delegate (CollisionObserver collisionObserver)
	//	{
	//		Collider collider = collisionObserver.getPreviousCollider();
	//		if (collider != null)
	//		{
	//			if (collider.gameObject.tag == "Player")
	//			{
	//				bool isEntering = collisionObserver.getIsEntering();
	//				TakeDamage takeDamageComponent = collider.gameObject.GetComponent<TakeDamage>();
	//				if(takeDamageComponent)
	//				{
	//					takeDamageComponent.applyDamage();
 //                   }
	//			}
	//		}
	//	};
	//	return m_collisionListener;
	//}
}
