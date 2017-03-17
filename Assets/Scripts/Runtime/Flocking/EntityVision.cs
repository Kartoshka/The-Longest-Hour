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

	public static string m_obstacleTag = "Terrain";
	public static string m_enemyTag = "Player";

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

	private GameObject GetClosestObject(List<GameObject> gameObjects)
	{
		if (gameObjects.Count > 0)
		{
			GameObject closestObject = gameObjects[0];
			foreach (GameObject obstacle in gameObjects)
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

	public GameObject GetClosestObstacle()
	{
		return GetClosestObject(m_observedObstacles);
	}

	public GameObject GetClosestEnemy()
	{
		return GetClosestObject(m_observedEnemies);
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	void OnTriggerEnter(Collider hit)
	{
		if(hit.tag.Equals(m_obstacleTag))
		{
			m_observedObstacles.Add(hit.gameObject);
		}
		else if(hit.tag.Equals(m_enemyTag))
		{
			m_observedEnemies.Add(hit.gameObject);
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if (hit.tag.Equals(m_obstacleTag))
		{
			m_observedObstacles.Remove(hit.gameObject);
		}
	}

	#endregion
}
