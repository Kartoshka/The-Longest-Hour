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

	public float m_timeScale = 1.0f;
	private Rigidbody m_rigidBody;

	private LinkedList<PhysicalState> m_recordedState = new LinkedList<PhysicalState>();
	private int m_recordLength = 200;
	private PhysicalState m_lastRecordedState;

	public bool m_isTimeBasedRecording = false;
	public bool m_isDistanceBasedRecording = true;

	public float m_positionalDifferenceThreshold = 0.01f;
	public float m_timeBetweenRecordings = 0.02f;

	private float m_timeSinceLastRecord = 0.0f;

	public bool isModifiable = false;

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
		if ((m_isTimeBasedRecording && m_timeSinceLastRecord <= 0) 
			|| (m_isDistanceBasedRecording && Vector3.Distance(m_rigidBody.position, m_lastRecordedState.position) > m_positionalDifferenceThreshold))
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
		PhysicsParameterController ctrler = FindObjectOfType<PhysicsParameterController> () as PhysicsParameterController;
		if (ctrler)
		{
			ctrler.register (this);
		} else
		{
			Debug.Log ("Warning, no global phyics controller found, " + this.gameObject.name + " may not be time controllable");
		}
    }

	void FixedUpdate()
	{
		processInputs();

		float usedScale = isModifiable ? m_timeScale : 1.0f;
		if (usedScale > 0)
		{
			m_rigidBody.velocity *= usedScale;
			m_rigidBody.angularVelocity *= usedScale;
			updateForward (Time.fixedDeltaTime * usedScale);
			m_rigidBody.isKinematic = false;

		} else if (usedScale == 0)
		{
			m_rigidBody.isKinematic = true;
		}
		else
		{
			m_rigidBody.isKinematic = false;

			m_rigidBody.velocity = Vector3.zero;
			m_rigidBody.angularVelocity = Vector3.zero;
			updateReverse(Time.fixedDeltaTime * usedScale);
		}
	}

	private void processInputs()
	{
//		if(Input.GetKey(KeyCode.Space))
//		{
//			m_timeScale = 0.1f;
//		}
//		else if(Input.GetKey(KeyCode.LeftShift))
//		{
//			m_timeScale = 0.0f;
//        }
//		else
//		{
//			m_timeScale = 1.0f;
//		}
//
//		if (Input.GetKey(KeyCode.LeftControl))
//		{
//			m_timeScale *= -1f;
//		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
