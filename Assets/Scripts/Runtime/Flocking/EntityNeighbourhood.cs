using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class EntityNeighbourhood : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	//public enum EntityType
	//{
	//	Player,
	//	Lemming,
	//	Enemy
	//}

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

	public Transform m_entityTransform;

	private List<GameObject> m_enemies = new List<GameObject>();
	private List<GameObject> m_allies = new List<GameObject>();

	private static string m_enemyTag = "Enemy";
	private static string m_allyTag = "Lemming";

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public string getEnemyTag() { return m_enemyTag; }
	public string getAllyTag() { return m_allyTag; }

	public List<GameObject> getAllies() { return m_allies; }
	public List<GameObject> getEnemies() { return m_enemies; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	void OnTriggerEnter(Collider hit)
	{
		if(this.tag.Equals(hit.gameObject.tag))
		{
			m_allies.Add(hit.gameObject);
        }
		else
		{
			m_enemies.Add(hit.gameObject);
		}
	}

	void OnTriggerExit(Collider hit)
	{
		if (this.tag.Equals(hit.gameObject.tag))
		{
			m_allies.Remove(hit.gameObject);
		}
		else
		{
			m_enemies.Remove(hit.gameObject);
		}
	}


	#endregion

	//public void Remove(GameObject gameObject){
	//	if(gameObject.tag.Equals("zombie")){
	//		_zombies.Remove(gameObject);
	//	}
	//	else if(gameObject.tag.Equals("survivor")){
	//		_survivors.Remove(gameObject);
	//	}
	//	else if(gameObject.tag.Equals("house")){
	//		_houses.Remove(gameObject);
	//	}
	//}

	//public GameObject GetClosestByType(EntityType eType){
	//	List<GameObject> objectList;
	//	switch(eType){
	//	case(EntityType.SURVIVOR):
	//		objectList = _survivors;
	//		break;
	//	case(EntityType.ZOMBIE):
	//		objectList = _zombies;
	//		break;
	//	case(EntityType.HOUSE):
	//		objectList = _houses;
	//		break;
	//	case(EntityType.HOUSE_CORNER):
	//		objectList = _houses;
	//		break;
	//	default:
	//		return null;
	//	}
	//	if(objectList.Count > 0){
	//		GameObject closestObject = objectList[0];
	//		foreach(GameObject obstacle in objectList){
	//			//Debug.DrawRay(body.transform.position, (obstacle.transform.position - body.transform.position)* Vector3.Distance(body.transform.position, obstacle.transform.position), Color.cyan);
	//			if(Vector3.SqrMagnitude(obstacle.transform.position - body.position) < Vector3.SqrMagnitude(closestObject.transform.position - body.position)){
	//				closestObject = obstacle;
	//			}
	//		}
	//		return closestObject;
	//	}
	//	else{
	//		return null;
	//	}
	//}


	///*
	//public GameObject GetClosestZombie(){
	//	if(_zombies.Count > 0){
	//		GameObject closestObject = _zombies[0];
	//		foreach(GameObject obstacle in _zombies){
	//			Debug.DrawRay(body.transform.position, (obstacle.transform.position - body.transform.position)* Vector3.Distance(body.transform.position, obstacle.transform.position), Color.cyan);
	//			if(Vector3.SqrMagnitude(obstacle.transform.position - body.position) < Vector3.SqrMagnitude(closestObject.transform.position - body.position)){
	//				closestObject = obstacle;
	//			}
	//		}
	//		return closestObject;
	//	}
	//	else{
	//		return null;
	//	}
	//}

	//public GameObject GetClosestSurvivor(){
	//	if(_survivors.Count > 0){
	//		GameObject closestObject = _survivors[0];
	//		foreach(GameObject obstacle in _survivors){
	//			Debug.DrawRay(body.transform.position, (obstacle.transform.position - body.transform.position)* Vector3.Distance(body.transform.position, obstacle.transform.position), Color.cyan);
	//			if(Vector3.SqrMagnitude(obstacle.transform.position - body.position) < Vector3.SqrMagnitude(closestObject.transform.position - body.position)){
	//				closestObject = obstacle;
	//			}
	//		}
	//		return closestObject;
	//	}
	//	else{
	//		return null;
	//	}
	//}
	//*/
}
