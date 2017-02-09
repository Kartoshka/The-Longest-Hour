using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MOJ.Helpers;

public class GrenadeControllerComponent : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public Transform m_projectileSource;
	public GameObject m_projectilePrefab;
	public float m_impulseMagnitude;
	public CollisionObserver m_collisionObserver;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	bool m_canFireProjectile = false;

	private Observer<CollisionObserver>.Listener m_collisionListener;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private void collectProjectile()
	{
		m_canFireProjectile = true;
	}

	void FireProjectile()
	{
		if (m_projectilePrefab)
		{
			GameObject spawnedObject = (GameObject)Instantiate(m_projectilePrefab, m_projectileSource.position, Quaternion.identity);
			if (spawnedObject)
			{
				Rigidbody rigidBody = spawnedObject.GetComponent<Rigidbody>();
				if (rigidBody)
				{
					rigidBody.velocity = m_projectileSource.forward * m_impulseMagnitude;
                }
			}
		}
	}

	// Listen to the CollisionObserver to detect pickups.
	private Observer<CollisionObserver>.Listener createCollisionListener()
	{
		m_collisionListener = delegate (CollisionObserver collisionObserver)
		{
			Collision collision = collisionObserver.getPreviousCollision();
			if (collision != null)
			{
				if (collision.gameObject.tag.Equals("Grenade"))
				{
					collectProjectile();
					Destroy(collision.gameObject);
				}
			}
		};
		return m_collisionListener;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		if(m_collisionObserver)
		{
			m_collisionListener = createCollisionListener();
			m_collisionObserver.getObserver().add(m_collisionListener);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (m_canFireProjectile && Input.GetButtonDown("Fire1"))
		{
			FireProjectile();
		}
	}

	#endregion
}
