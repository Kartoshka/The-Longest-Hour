using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using MOJ.Helpers;

public class GrenadeControllerComponent : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public Transform m_projectileSource;
	public GameObject m_projectilePrefab;
	public float m_impulseMagnitude;
	public float cooldown=1;
	private float lastFired = -1;

	public CollisionObserver m_collisionObserver;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private bool m_canFireProjectile = true;

	private Observer<GrenadeControllerComponent> m_observer;    // For the networked component to be notified when projectile is fired.
	private Observer<CollisionObserver>.Listener m_collisionListener;   // For listening to when the character has picked up a projectile.

	private GameObject m_previouslySpawnedProjectile;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public Observer<GrenadeControllerComponent> getObserver() { return m_observer; }

	//public Transform getProjectileSource() { return m_projectileSource; }
	//public GameObject getProjectilePrefab() { return m_projectilePrefab; }
	//public float getImpulseMagnitude() { return m_impulseMagnitude; }
	public GameObject getSpawnedProjectile() { return m_previouslySpawnedProjectile; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private void collectProjectile()
	{
		m_canFireProjectile = true;
	}

	public void FireProjectile()
	{
		if ((Time.time - lastFired) > cooldown)
		{
			lastFired = Time.time;
			//if(Network.peerType.Equals(NetworkPeerType.Disconnected))
			//{
			if (m_projectilePrefab)
			{
				//GameObject spawnedObject = (GameObject)Instantiate(m_projectilePrefab, m_projectileSource.position, Quaternion.identity);
				m_previouslySpawnedProjectile = NetworkLobbyManager.Instantiate<GameObject> (m_projectilePrefab, m_projectileSource.position, Quaternion.identity);
				if (m_previouslySpawnedProjectile)
				{
					Rigidbody rigidBody = m_previouslySpawnedProjectile.GetComponent<Rigidbody> ();
					if (rigidBody)
					{
						rigidBody.velocity = m_projectileSource.forward * m_impulseMagnitude;
					}
				}
			}
			//}
			//else
			//{
			//	// If networked, move functionality to networked component.
			m_observer.notify ();
		}
		//}
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

	void Awake()
	{
		m_observer = new Observer<GrenadeControllerComponent>(this);
	}

	public void initialize()
	{
		if (m_collisionObserver)
		{
			m_collisionListener = createCollisionListener();
			m_collisionObserver.getObserver().add(m_collisionListener);
		}
	}

	// Use this for initialization
	void Start()
	{
		initialize();
	}

	public void update()
	{
//		if (m_canFireProjectile && Input.GetButtonDown("Fire1"))
//		{
//			FireProjectile();
//		}
	}

	// Update is called once per frame
	void Update()
	{
		update();
    }

	#endregion
}
