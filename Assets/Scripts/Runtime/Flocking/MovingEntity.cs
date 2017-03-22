using UnityEngine;
using System.Collections;

public class MovingEntity : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	protected Vector3 m_velocity = Vector3.zero;
	public float m_mass = 1.0f;
	public float m_maxSpeed = 1.0f; //maximum travel speed
	public float m_maxForce = 1.0f; //maximum force entity can produce to power itself
	public float m_maxTurnRate = 0.5f; //radians per second

	public float m_targetRangeThreshold = 1.0f;
	public bool m_constrainYAxisRotation = true;

	//	private GameWorld _gameWorld; //Reference to the GameWorld to view objects
	private SteeringBehaviours m_steering; //The steering behaviours this entity uses

	private bool m_canUpdate = true;

	public CollisionObserver m_collisionObserver;
	private Observer<CollisionObserver>.Listener m_collisionListener;

	private bool m_isInTargetRange = false;
	private GameObject m_targetObject;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public Vector3 getVelocity() { return m_velocity; }

	public float getMaxSpeed() { return m_maxSpeed; }

	public float getMaxForce() { return m_maxForce; }

	public void setCanUpdate(bool canUpdate) { m_canUpdate = canUpdate; }

	public GameObject getTargetObject()
	{
		return m_targetObject;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	private Observer<CollisionObserver>.Listener createCollisionListener()
	{
		m_collisionListener = delegate (CollisionObserver collisionObserver)
		{
			Collider collider = collisionObserver.getPreviousCollider();
			if (collider != null)
			{
				bool success = false;
				if (collider.gameObject.tag == "Player")
				{
					success = true;
				}
				else if(m_steering)
				{
					if(collider.gameObject.Equals(m_steering.getPursuitTarget()))
					{
						success = true;
                    }
				}
				if(success)
				{
					bool isEntering = collisionObserver.getIsEntering();
					m_isInTargetRange = isEntering;
					m_targetObject = collider.gameObject;
                }
			}
		};
		return m_collisionListener;
	}

	public bool isInTargetRange()
	{
		//bool isInRange = false;
		//if (m_steering && 
		//	(m_steering.m_isPursuing || m_steering.m_isPathfinding))
		//{
		//	isInRange = Vector3.Distance(this.transform.position, m_steering.getPursuitTarget().transform.position) < m_targetRangeThreshold;
		//}
		//return isInRange;

		return m_isInTargetRange;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		m_steering = this.GetComponent<SteeringBehaviours>();

		if (m_collisionObserver)
		{
			m_collisionListener = createCollisionListener();
			Observer<CollisionObserver> observer = m_collisionObserver.getObserver();
			observer.add(m_collisionListener);
		}
	}

	public void update()
	{
		if(m_canUpdate)
		{
			Vector3 steeringForce = m_steering.CalculateSteeringForce();
			Vector3 acceleration = steeringForce / m_mass;
			m_velocity += acceleration * Time.deltaTime;
			float magnitude = Vector3.Magnitude(m_velocity);
			if (magnitude > m_maxSpeed)
			{
				m_velocity -= (m_velocity.normalized * (magnitude - m_maxSpeed));
			}
			Vector3 position = this.transform.position + m_velocity;// * Time.deltaTime;
			this.transform.position = position;
			if (m_velocity.magnitude > 0.0001f)
			{
				if(m_constrainYAxisRotation)
				{
					m_velocity.y = 0;
				}
                this.transform.forward = m_velocity.normalized; //Rotate the entity towards direction travelled
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		update();
    }

	#endregion
}
