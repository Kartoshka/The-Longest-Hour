using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearExplosionAttack : MonoBehaviour {

	public float forceAmount = 20;
	public float radius = 1f;
	public CollisionObserver m_collisionObserver;
	private Observer<CollisionObserver>.Listener m_collisionListener;

	// Use this for initialization
	void Start () {
		if (m_collisionListener==null && m_collisionObserver!=null)
		{
			Observer<CollisionObserver> observer = m_collisionObserver.getObserver();
			m_collisionListener = createCollisionListener ();
			observer.add(m_collisionListener);
			m_collisionObserver.gameObject.SetActive(false);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private Observer<CollisionObserver>.Listener createCollisionListener()
	{
		m_collisionListener = delegate (CollisionObserver collisionObserver)
		{
			Collider collider = collisionObserver.getPreviousCollider();
			if (collider != null && collider.tag!="Player")
			{
				collider.attachedRigidbody.AddExplosionForce(forceAmount,collisionObserver.transform.position,radius);
			}
		};
		return m_collisionListener;
	}

	private static bool IsInLayerMask(int layer, LayerMask layermask)
	{
		return layermask == (layermask | (1 << layer));
	}

}
