using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsTimeCtrl : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	public struct PhysicalState
	{
		public PhysicalState(Vector3 position, Quaternion rotation, Vector3 velocity)
		{
			this.position = position;
			this.rotation = rotation;
			this.velocity = velocity;
		}

		public Vector3 position;
		public Quaternion rotation;
		public Vector3 velocity;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private float m_timeScale = 1.0f;
	private Rigidbody m_rigidBody;

	private LinkedList<PhysicalState> m_recordedState = new LinkedList<PhysicalState>();
	private int m_recordLength = 200;
	private PhysicalState m_lastRecordedState;

	private float m_timeBetweenRecordings = 0.02f;
	private float m_timeSinceLastRecord = 0.0f;

	// Debug: TODO: Remove this.
	public bool m_isReverse = false;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public void setTimeScale(float timeScale) { m_timeScale = timeScale; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private void recordState()
	{
		PhysicalState state;
		state.position = m_rigidBody.position;
		state.rotation = m_rigidBody.rotation;
		state.velocity = m_rigidBody.velocity;
		//m_recordedState.Push(state);
		m_recordedState.AddFirst(new LinkedListNode<PhysicalState>(state));
		if(m_recordedState.Count > m_recordLength)
		{
			m_recordedState.RemoveLast();
		}
    }

	private void playPreviousRecord()
	{
		if(m_recordedState.Count > 0)
		{
			PhysicalState state = m_recordedState.First.Value;
			m_lastRecordedState = state;
			m_recordedState.RemoveFirst();
		}

		m_rigidBody.position = m_lastRecordedState.position;
		m_rigidBody.rotation = m_lastRecordedState.rotation;
		m_rigidBody.velocity = m_lastRecordedState.velocity;
	}

	public void updateForward(float deltaTime)
	{
		m_timeSinceLastRecord -= deltaTime;
		if (m_timeSinceLastRecord <= 0)
		{
			m_timeSinceLastRecord = m_timeBetweenRecordings;
			recordState();
		}
	}

	public void updateReverse(float deltaTime)
	{
		m_timeSinceLastRecord -= deltaTime;
		if (m_timeSinceLastRecord >= m_timeBetweenRecordings)
		{
			m_timeSinceLastRecord = 0;
			playPreviousRecord();
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		m_rigidBody = GetComponent<Rigidbody>();
    }

	void FixedUpdate()
	{
		processInputs();

		if (m_timeScale > 0)
		{
			m_isReverse = false;

            m_rigidBody.velocity *= m_timeScale;
			m_rigidBody.angularVelocity *= m_timeScale;
			updateForward(Time.fixedDeltaTime * m_timeScale);
        }
		else
		{
			m_isReverse = true;

			m_rigidBody.velocity = Vector3.zero;
			m_rigidBody.angularVelocity = Vector3.zero;
			updateReverse(Time.fixedDeltaTime * m_timeScale);
		}
	}

	private void processInputs()
	{
		if(Input.GetKey(KeyCode.Space))
		{
			m_timeScale = 0.1f;
		}
		else if(Input.GetKey(KeyCode.LeftShift))
		{
			m_timeScale = 0.0f;
        }
		else
		{
			m_timeScale = 1.0f;
		}

		if (Input.GetKey(KeyCode.LeftControl))
		{
			m_timeScale *= -1f;
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
