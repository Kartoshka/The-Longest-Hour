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

	//	private GameWorld _gameWorld; //Reference to the GameWorld to view objects
	private SteeringBehaviours m_steering; //The steering behaviours this entity uses

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public Vector3 getVelocity() { return m_velocity; }

	public float getMaxSpeed() { return m_maxSpeed; }

	public float getMaxForce() { return m_maxForce; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		m_steering = this.GetComponent<SteeringBehaviours>();
	}

	// Update is called once per frame
	void Update()
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
			this.transform.forward = m_velocity.normalized; //Rotate the entity towards direction travelled
		}
	}

	#endregion
}
