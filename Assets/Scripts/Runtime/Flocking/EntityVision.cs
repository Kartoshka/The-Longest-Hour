using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityVision : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public Transform m_sightSource;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private List<GameObject> m_observedObstacles = new List<GameObject>();
	private static string m_observationTag = "terrain"; 

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public List<GameObject> getObstacles() { return m_observedObstacles; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	public GameObject GetClosestObstacle()
	{
		if (m_observedObstacles.Count > 0)
		{
			GameObject closestObject = m_observedObstacles[0];
			foreach (GameObject obstacle in m_observedObstacles)
			{
				//Debug.DrawRay(body.transform.position, (obstacle.transform.position - body.transform.position)* Vector3.Distance(body.transform.position, obstacle.transform.position), Color.cyan);
				if (Vector3.SqrMagnitude(obstacle.transform.position - m_sightSource.position) < Vector3.SqrMagnitude(closestObject.transform.position - m_sightSource.position))
				{
					closestObject = obstacle;
				}
			}
			return closestObject;
		}
		else
		{
			return null;
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	void OnTriggerEnter(Collider hit)
	{
		if (hit.tag.Equals(m_observationTag))
		{
			m_observedObstacles.Add(hit.gameObject);
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if (hit.tag.Equals(m_observationTag))
		{
			m_observedObstacles.Remove(hit.gameObject);
		}
	}

	#endregion
}
