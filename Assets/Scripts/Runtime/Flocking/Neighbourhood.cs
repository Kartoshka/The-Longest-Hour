using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EntityType{
	SURVIVOR,
	ZOMBIE,
	HOUSE,
	HOUSE_CORNER
}

public class Neighbourhood : MonoBehaviour {
	//private List<GameObject> _survivors = new List<GameObject>();
	//private List<GameObject> _zombies = new List<GameObject>();
	//private List<GameObject> _houses = new List<GameObject>();
	//private Transform body;

	//// Use this for initialization
	//void Start () {
	//	body = this.transform.parent;
	//}

	//void OnTriggerEnter(Collider hit){
	//	if(hit.tag.Equals("zombie")){
	//		_zombies.Add(hit.gameObject);
	//	}
	//	else if(hit.tag.Equals("survivor")){
	//		_survivors.Add(hit.gameObject);
	//	}
	//	else if(hit.tag.Equals("house")){
	//		_houses.Add(hit.gameObject);
	//	}
	//}
	
	//void OnTriggerExit(Collider hit){
	//	if(hit.tag.Equals("zombie")){
	//		_zombies.Remove(hit.gameObject);
	//	}
	//	else if(hit.tag.Equals("survivor")){
	//		_survivors.Remove(hit.gameObject);
	//	}
	//	else if(hit.tag.Equals("house")){
	//		_houses.Remove(hit.gameObject);
	//	}
	//}

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
	
	//public List<GameObject> GetSurvivors(){
	//	return _survivors;
	//}

	//public List<GameObject> GetZombies(){
	//	return _zombies;
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
