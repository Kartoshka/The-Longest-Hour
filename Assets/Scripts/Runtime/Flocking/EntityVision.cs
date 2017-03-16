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

	public const string m_obstacleTag = "Ground";
	public const string m_enemyTag = "Player";

	private List<GameObject> m_observedObstacles = new List<GameObject>();
	private List<GameObject> m_observedEnemies = new List<GameObject>();

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
		switch(hit.tag)
		{
			case (m_obstacleTag):
			{
				m_observedObstacles.Add(hit.gameObject);
				break;
			}
			case (m_enemyTag):
			{
				m_observedEnemies.Add(hit.gameObject);
				break;
			}
		}
	}

	void OnTriggerExit(Collider hit)
	{
		switch (hit.tag)
		{
			case (m_obstacleTag):
			{
				m_observedObstacles.Remove(hit.gameObject);
				break;
			}
		}
	}

	#endregion
}
