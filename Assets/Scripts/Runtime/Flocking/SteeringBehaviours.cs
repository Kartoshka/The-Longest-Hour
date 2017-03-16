
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SteeringBehaviours : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public EntityNeighbourhood m_entityNeighbourhoodObject;
	public MovingEntity m_movingEntityObject;
	public GameObject m_visionColliderObject;
	public EntityVision m_visionObject;


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private MovingEntity m_movingEntity;
	public GameObject m_target;

	public bool m_isPursuing = true;

	public bool m_isEvading = true;
	public float m_fleeDistance;

	public bool m_isWandering = true;
	public float m_wanderRadius = 1.0f;
	public float m_wanderDistance = 2.0f;
	public float m_wanderJitter = 0.25f;
	public float m_wanderUpdateRate = 1.0f;
	private float m_wanderUpdateRateTimer = 0.0f;
	private Vector3 m_wanderTarget;

	public bool m_isAvoidingObstacles = true;
	public float m_minVisionLength = 1.5f;
	public float m_maxVisionLength = 5.0f;
	private GameObject m_visionCollider;
	private EntityVision m_vision;
	private GameObject m_closestObstacle;
	private float m_minTurnWeight = 0.1f;
	private float m_currentTurnWeight = 0.1f;
	private Vector3 m_previousPosition;

	public bool m_isPathfinding = true;
	public List<GameObject> m_wayPoints;
	public float m_arrivalThreshold = 0.1f;
	private int m_currentWaypoint = 0;

	private EntityNeighbourhood m_neighbourhood;
	public bool m_separation = true;
	public bool m_alignment = true;
	public bool m_cohesion = true;

	private Vector3 m_steeringForce;

	private Observer<SteeringBehaviours> m_observer;


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public void setIsWandering(bool isWandering)
	{
		m_isWandering = isWandering;
	}

	public void setIsPathFinding(bool isPathfinding)
	{
		m_isPathfinding = isPathfinding;
	}

	public void AddWaypoint(GameObject waypoint)
	{
		m_isPathfinding = true;
		m_wayPoints.Add(waypoint);
	}

	public void ClearWaypoints()
	{
		m_wayPoints.Clear();
	}

	public void setPursuitTarget(bool pursueImmediate, GameObject target)
	{
		m_isPursuing = pursueImmediate;
		if (m_isPursuing)
		{
			m_target = target;
		}
	}

	public Observer<SteeringBehaviours> getObserver() { return m_observer; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private Vector3 Seek(Vector3 targetPosition)
	{
		Vector3 desiredVelocity = Vector3.Normalize(targetPosition - this.transform.position) * m_movingEntity.getMaxSpeed();
		//Debug.DrawRay(this.transform.position, desiredVelocity*3, Color.red);
		return (desiredVelocity - m_movingEntity.getVelocity());
	}

	private Vector3 Flee(Vector3 targetPosition)
	{
		float panicDistance = m_fleeDistance * m_fleeDistance;
		if (Vector3.Distance(this.transform.position, m_target.transform.position) > panicDistance)
		{
			return Vector3.zero;
		}
		Vector3 desiredVelocity = Vector3.Normalize(this.transform.position - targetPosition) * m_movingEntity.getMaxSpeed();
		//Debug.DrawRay(this.transform.position, desiredVelocity*3, Color.yellow);
		return (desiredVelocity - m_movingEntity.getVelocity());
	}

	private Vector3 Arrive(Vector3 targetPosition, float deceleration)
	{
		Vector3 toTarget = targetPosition - this.transform.position;
		float distance = toTarget.magnitude;
		if (distance > 0)
		{
			float speed = distance / deceleration;
			speed = Mathf.Min(speed, m_movingEntity.getMaxSpeed());
			Vector3 desiredVelocity = toTarget * speed / distance;
			return (desiredVelocity - m_movingEntity.getVelocity());
		}
		return Vector3.zero;
	}

	private Vector3 Pursuit(GameObject evader)
	{
		Vector3 toEvader = evader.transform.position - this.transform.position;
		float relativeHeading = Vector3.Dot(this.transform.forward, evader.transform.forward);

		//If the evader is heading for us, steer directly to the evader
		float radAngle = 0.95f; //acos(0.95)=18 degrees 
		if (Vector3.Dot(toEvader, this.transform.forward) > 0 && relativeHeading < -radAngle)
		{
			return Seek(evader.transform.position); //Was: Seek
		}

		//predict where the evader will be
		Vector3 futurePosition;
		if (evader.GetComponent<MovingEntity>())
		{
			Vector3 evaderVelocity = evader.GetComponent<MovingEntity>().getVelocity();
			float evaderSpeed = evaderVelocity.magnitude;
			float lookAheadTime = toEvader.magnitude / (m_movingEntity.getMaxSpeed() + evaderSpeed);
			lookAheadTime += TurnAroundTime(this.gameObject, evader.transform.position);
			futurePosition = evaderVelocity * lookAheadTime;
		}
		else
		{ 
			//if the evader isn't a moving entity
			futurePosition = Vector3.zero;
		}
		return Seek(evader.transform.position + futurePosition);
	}

	private float TurnAroundTime(GameObject vehicle, Vector3 targetPosition)
	{
		Vector3 toTarget = Vector3.Normalize(targetPosition = vehicle.transform.position);
		float dot = Vector3.Dot(vehicle.transform.forward, toTarget); //1 if target directly ahead, -1 if directly behind
		float turnRate = 0.5f; //heading opposite direction to target means 0.5 will take 1 second
		return (dot - 1) * -turnRate;
	}

	private Vector3 Evade(GameObject pursuer)
	{
		Vector3 toPursuer = pursuer.transform.position - this.transform.position;

		//flee from the predicted future position of the pursuer
		Vector3 futurePosition;
		if (pursuer.GetComponent<MovingEntity>())
		{
			Vector3 pursuerVelocity = pursuer.GetComponent<MovingEntity>().getVelocity();
			float pursuerSpeed = pursuerVelocity.magnitude;
			float lookAheadTime = toPursuer.magnitude / (m_movingEntity.getMaxSpeed() + pursuerSpeed);
			futurePosition = pursuerVelocity * lookAheadTime;
		}
		else
		{ //if the pursuer isn't a moving entity
			futurePosition = Vector3.zero;
		}
		return Flee(pursuer.transform.position + futurePosition);
	}

	private Vector3 Wander()
	{
		m_wanderUpdateRateTimer -= Time.deltaTime;
		if (m_wanderUpdateRateTimer <= 0.0f)
		{
			m_wanderTarget = new Vector3(Random.Range(-1.0f, 1.0f) * m_wanderJitter, 0, Random.Range(-1.0f, 1.0f) * m_wanderJitter);
			m_wanderUpdateRateTimer = m_wanderUpdateRate;
		}
		m_wanderTarget = Vector3.Normalize(m_wanderTarget);
		m_wanderTarget *= m_wanderRadius;
		Vector3 target = m_wanderTarget + this.transform.forward * m_wanderDistance;
		//Debug.DrawRay(this.transform.position, target, Color.green);
		return target;
	}

	private Vector3 Obstacle_Avoidance()
	{
		//the detection box length is proportional to the agent's velocity
		float speed = this.GetComponent<MovingEntity>().getVelocity().magnitude;
		float minDetectionLength = m_minVisionLength;
		float maxDetectionLength = m_maxVisionLength + this.GetComponent<MovingEntity>().getMaxSpeed() * 30; //If the speed is higher, the vision cube should be longer to avoid fast-approaching walls
		m_visionCollider.transform.localScale = new Vector3(1.5f, 1.0f, minDetectionLength) + (speed / this.GetComponent<MovingEntity>().getMaxSpeed()) * (new Vector3(1.5f, 1.0f, maxDetectionLength));

		m_closestObstacle = m_vision.GetClosestObstacle();
		Vector3 avoidanceForce = Vector3.zero;
		if (m_closestObstacle)
		{
			//--steering force is proportional to proximity--//
			float visionLength = m_visionCollider.transform.localScale.z;
			float distanceToObject = Vector3.Distance(this.transform.position, m_closestObstacle.transform.position);
			//--lateral force--//
			float deltaThreshold = 0.0075f + this.GetComponent<MovingEntity>().getMaxSpeed() / 4;
			if (Vector3.SqrMagnitude(m_previousPosition - this.transform.position) < deltaThreshold)
			{
				m_currentTurnWeight = Random.Range(m_currentTurnWeight, m_currentTurnWeight + 0.2f);
				Debug.Log("stuck");
			}
			else
			{
				m_currentTurnWeight = m_minTurnWeight; //Random.Range(_minTurnWeight, _minTurnWeight + 0.2f);  
			}

			//-Determine if the obstacle is on the left or right side of the forward vector (reference http://forum.unity3d.com/threads/left-right-test-function.31420/ )-//
			Vector3 targetVector = m_closestObstacle.transform.position - this.transform.position;
			Vector3 cross = Vector3.Cross(this.transform.forward, targetVector);
			float rightwards = Vector3.Dot(cross, this.transform.up); //rightwards == 1 if target is to the right of forward direction, -1 if leftwards, 0 if exactly in front
			m_previousPosition = this.transform.position;
			if (rightwards > 0.0f)
			{
				m_currentTurnWeight *= -1.0f;
			}
			avoidanceForce = m_currentTurnWeight * this.transform.right * (visionLength / Mathf.Max(0.0001f, distanceToObject)) * m_closestObstacle.transform.localScale.x;
			//--backwards force--//
			float brakeWeight = 0.2f;
			avoidanceForce -= brakeWeight * this.transform.forward * (visionLength / Mathf.Max(0.0001f, distanceToObject)) * m_closestObstacle.transform.localScale.x;
		}
		//Debug.DrawRay(this.transform.position, avoidanceForce*2, Color.cyan);
		return avoidanceForce;
	}

	public Vector3 Follow_Path()
	{
		if (m_wayPoints.Count > 0)
		{
			if (m_currentWaypoint < m_wayPoints.Count)
			{
				if (Vector3.SqrMagnitude(m_wayPoints[m_currentWaypoint].transform.position - this.transform.position) < m_arrivalThreshold)
				{
					++m_currentWaypoint;
					if (m_currentWaypoint == m_wayPoints.Count)
					{
						return Seek(m_wayPoints[m_currentWaypoint - 1].transform.position);
					}
				}
				return Seek(m_wayPoints[m_currentWaypoint].transform.position);
			}
			else
			{
				return Arrive(m_wayPoints[m_currentWaypoint - 1].transform.position, 1);
			}
		}
		return Vector3.zero;
	}

	public Vector3 Group_Separation()
	{
		Vector3 steeringForce = Vector3.zero;
		List<GameObject> neighbours = GetSimilarNeighbours();
		neighbours.AddRange(GetEnemyNeighbours());
		if (neighbours != null)
		{
			for (int i = 0; i < neighbours.Count; ++i)
			{
				Vector3 toSelf = this.transform.position - neighbours[i].transform.position;
				steeringForce += toSelf.normalized / toSelf.magnitude; //Force is inversely proportional to distance from neighbour
			}
		}
		return steeringForce;
	}

	public Vector3 Group_Alignment()
	{
		Vector3 averageHeading = Vector3.zero;
		List<GameObject> neighbours = GetSimilarNeighbours();
		if (neighbours != null && neighbours.Count > 0)
		{
			for (int i = 0; i < neighbours.Count; ++i)
			{
				averageHeading += neighbours[i].transform.forward;
			}
			averageHeading /= (float)neighbours.Count;
			averageHeading -= this.transform.forward;
		}
		return averageHeading;
	}

	public Vector3 Group_Cohesion()
	{
		Vector3 centerOfMass = Vector3.zero;
		Vector3 steeringForce = Vector3.zero;

		List<GameObject> neighbours = GetSimilarNeighbours();
		if (neighbours != null && neighbours.Count > 0)
		{
			for (int i = 0; i < neighbours.Count; ++i)
			{
				centerOfMass += neighbours[i].transform.position;
			}
			centerOfMass /= (float)neighbours.Count;
			steeringForce = Seek(centerOfMass);
		}
		return steeringForce;
	}

	public Vector3 CalculateSteeringForce()
	{
		m_steeringForce = Vector3.zero;

		if (m_isPursuing)
		{
			AccumulateForce(ref m_steeringForce, Pursuit(m_target));
		}
		if (m_isEvading)
		{
			AccumulateForce(ref m_steeringForce, Evade(m_target));
		}
		if (m_isWandering)
		{
			AccumulateForce(ref m_steeringForce, Wander());
		}
		if (m_isAvoidingObstacles)
		{
			AccumulateForce(ref m_steeringForce, Obstacle_Avoidance());
		}
		if (m_isPathfinding)
		{
			AccumulateForce(ref m_steeringForce, Follow_Path());
		}
		if (m_separation)
		{
			AccumulateForce(ref m_steeringForce, Group_Separation());
		}
		if (m_alignment)
		{
			AccumulateForce(ref m_steeringForce, Group_Alignment());
		}
		if (m_cohesion)
		{
			AccumulateForce(ref m_steeringForce, Group_Cohesion());
		}

		return m_steeringForce;
	}

	private bool AccumulateForce(ref Vector3 currentForce, Vector3 forceToAdd)
	{
		//How much steering force has this entity used so far
		float magnitudeSoFar = currentForce.magnitude;
		//How much steering force remains to be used
		float magnitudeRemaining = m_movingEntity.getMaxForce() - magnitudeSoFar;
		//return false if there's no more force left to use
		if (magnitudeRemaining <= 0.0f) return false;

		float magnitudeToAdd = forceToAdd.magnitude;
		if (magnitudeToAdd < magnitudeRemaining)
		{
			currentForce += forceToAdd;
		}
		else
		{
			currentForce += Vector3.Normalize(forceToAdd) * magnitudeRemaining;
		}
		return true;
	}

	private List<GameObject> GetSimilarNeighbours()
	{
		return m_neighbourhood.getAllies();
	}

	private List<GameObject> GetEnemyNeighbours()
	{
		return m_neighbourhood.getEnemies();
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	void Awake()
	{
		m_observer = new Observer<SteeringBehaviours>(this);
	}

	// Use this for initialization
	void Start()
	{
		m_movingEntity = m_movingEntityObject;
		m_visionCollider = m_visionColliderObject;
		m_vision = m_visionObject;
		m_neighbourhood = m_entityNeighbourhoodObject;
		m_previousPosition = m_movingEntity.transform.position;
	}

	#endregion

	//public void Update_Path(){
	//	_wayPoints.Clear();
	//	//TODO: Look for more waypoints
	//}

}
