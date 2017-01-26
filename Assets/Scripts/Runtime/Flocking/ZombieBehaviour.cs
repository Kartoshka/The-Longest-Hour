using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ZombieState{
	IDLE,
	WANDER,
	ATTACKING_SURVIVOR,
	SEEKING_SURVIVOR,
	SEARCHING_HOUSE,
	EXITING_HOUSE,
	ATTACKING_DOOR,
	ESCAPING
}

public class ZombieBehaviour : MonoBehaviour {
	//public ZombieState _state = ZombieState.IDLE;	
	//private SteeringBehaviours _steering; //The steering behaviours this entity uses
	//private Neighbourhood _neighbourhood; //The entities that are within the entity's vision

	//// -- Wandering -- //
	//private float _wanderTime = 0.5f;
	//private float _wanderTimer = 0.5f;

	//// -- ATTACKING_DOOR -- //
	//private GameObject _target;
	//private GameObject _targetHouse;
	//private GameObject _targetWaypoint;
	//private bool _waypointIsSet = false;
	//private WaypointType _waypointType = WaypointType.DOOR;

	//// Use this for initialization
	//void Start () {
	//	_steering = this.GetComponent<SteeringBehaviours>();
	//	_neighbourhood = GetComponentInChildren<Neighbourhood>();
	//	_state = ZombieState.IDLE;
	//}

	//private List<GameObject> GetVisibleWaypointsByType(GameObject house, WaypointType type){
	//	List<GameObject> waypoints = new List<GameObject>();
	//	Transform waypointGroup = house.transform.FindChild("waypoints");
	//	RaycastHit hit;
	//	string waypointName = "null";
	//	if(waypointGroup){
	//		for(int i = 0; i < waypointGroup.childCount; ++i){
	//			switch(type){
	//			case(WaypointType.CORNER):
	//				waypointName = "waypoint_corner";
	//				break;
	//			case(WaypointType.DOOR):
	//				waypointName = "waypoint_door";
	//				break;
	//			case(WaypointType.ROOM):
	//				waypointName = "waypoint_room";
	//				break;
	//			}
	//			if(waypointGroup.GetChild(i).name.Equals(waypointName)){
	//				if( Physics.Raycast(this.transform.position, waypointGroup.GetChild(i).transform.position - this.transform.position, out hit, 100) ){
	//					if(hit.collider.name.Equals(waypointName)){
	//						waypoints.Add(waypointGroup.GetChild(i).gameObject);
	//					}
	//				}
	//			}
	//		}
	//	}
	//	return waypoints;
	//}

	//private GameObject GetClosestWaypoint(List<GameObject> waypoints, float minDistanceIgnore){
	//	RaycastHit hit;
	//	GameObject closestWaypoint = waypoints[0];
	//	for(int i = 1; i < waypoints.Count; ++i){
	//		GameObject waypoint = waypoints[i];
	//		if( Physics.Raycast(this.transform.position, waypoint.transform.position - this.transform.position, out hit, 100) ){
	//			if(hit.distance > minDistanceIgnore){
	//				closestWaypoint = waypoint;
	//			}
	//		}
	//	}
	//	return closestWaypoint;
	//}

	//private List<GameObject> GetVisibleSurvivors(List<GameObject> survivors){
	//	_neighbourhood.GetSurvivors();
	//	List<GameObject> visibleSurvivors = new List<GameObject>();
	//	RaycastHit hit;
	//	foreach(GameObject survivor in survivors){
	//		if( Physics.Raycast(this.transform.position, survivor.transform.position - this.transform.position, out hit, 100) ){
	//			if(hit.collider.tag.Equals("survivor")){
	//				visibleSurvivors.Add(survivor);
	//			}
	//		}
	//	}
	//	return visibleSurvivors;
	//}
	
	//public void StateMachine(){
	//	switch(_state){
	//	case(ZombieState.IDLE):
	//		_steering.setIsWandering(true);
	//		_state = ZombieState.WANDER;
	//		break;
	//	case(ZombieState.WANDER):
	//		_wanderTimer -= Time.deltaTime;
	//		if(_wanderTimer < 0.0f){
	//			_wanderTimer = _wanderTime;
	//			//Look for a house to search
	//			GameObject target = _neighbourhood.GetClosestByType(EntityType.SURVIVOR);
	//			if(target != null){ 
	//				_state = ZombieState.ATTACKING_SURVIVOR;
	//				_steering.setPursuitTarget(true, target);
	//				_steering.setIsWandering(false);
	//			}
	//		}
	//		break;
	//	case(ZombieState.SEEKING_SURVIVOR):
	//		//Check if survivor is still visible
	//		List<GameObject> survivors = GetVisibleSurvivors(_neighbourhood.GetSurvivors());
	//		if(survivors.Count > 0){
	//			_steering.setPursuitTarget(true, _neighbourhood.GetClosestByType(EntityType.SURVIVOR) );
	//			_state = ZombieState.ATTACKING_SURVIVOR;
	//		}
	//		else{
	//			_targetHouse = _neighbourhood.GetClosestByType(EntityType.HOUSE);
	//			if(_targetHouse != null){ 
	//				_state = ZombieState.SEARCHING_HOUSE;
	//				_steering.setIsWandering(false);
	//			}
	//			else{
	//				_steering.setIsWandering(true);
	//				_state = ZombieState.WANDER;
	//			}
	//		}
	//		break;
	//	case(ZombieState.ATTACKING_SURVIVOR):
	//		_wanderTimer -= Time.deltaTime;
	//		if(_wanderTimer < 0.0f){
	//			_wanderTimer = _wanderTime;
	//			_state = ZombieState.SEEKING_SURVIVOR;
	//			_steering.setPursuitTarget(false, null);
	//		}
	//		break;
	//	case(ZombieState.SEARCHING_HOUSE):
	//		if(!_waypointIsSet){
	//			//Search for a waypoint on the house, by priority
	//			List<GameObject> waypoints = GetVisibleWaypointsByType(_targetHouse, WaypointType.ROOM);
	//			if(waypoints == null || waypoints.Count < 1){
	//				waypoints = GetVisibleWaypointsByType(_targetHouse, WaypointType.DOOR);
	//				if(waypoints == null || waypoints.Count < 1){
	//					waypoints = GetVisibleWaypointsByType(_targetHouse, WaypointType.CORNER);
	//				}
	//			}
	//			//No waypoint found. Walk for a bit.
	//			if(waypoints == null || waypoints.Count < 1){
	//				_state = ZombieState.WANDER;
	//			}
	//			//Waypoint found. Go to it.
	//			else{
	//				_targetWaypoint = GetClosestWaypoint(waypoints, 1.0f);
	//				_steering.AddWaypoint(_targetWaypoint);
	//				_waypointIsSet = true;
	//			}
	//		}
	//		//Going to the next waypoint
	//		else{
	//			Debug.DrawRay(this.transform.position, _targetWaypoint.transform.position - this.transform.position, Color.red);
	//			//Arrived at waypoint
	//			if(Vector3.SqrMagnitude(_targetWaypoint.transform.position - this.transform.position) < 2.0f){
	//				_waypointIsSet = false;
	//				_steering.ClearWaypoints();
	//				if(_targetWaypoint.name.Equals("waypoint_room")){
	//					_state = ZombieState.EXITING_HOUSE;
	//				}
	//			}
	//		}
	//		break;
	//	case(ZombieState.EXITING_HOUSE):
	//		if(!_waypointIsSet){
	//			//Search for a waypoint on the house, by priority
	//			List<GameObject> waypoints = GetVisibleWaypointsByType(_targetHouse, WaypointType.DOOR);
	//			//No waypoint found. Walk for a bit.
	//			if(waypoints == null || waypoints.Count < 1){
	//				_state = ZombieState.WANDER;
	//			}
	//			//Waypoint found. Go to it.
	//			else{
	//				_targetWaypoint = GetClosestWaypoint(waypoints, 1.0f);
	//				_steering.AddWaypoint(_targetWaypoint);
	//				_waypointIsSet = true;
	//			}
	//		}
	//		//Going to the next waypoint
	//		else{
	//			//Arrived at waypoint
	//			if(Vector3.SqrMagnitude(_targetWaypoint.transform.position - this.transform.position) < 2.0f){
	//				_waypointIsSet = false;
	//				_steering.ClearWaypoints();
	//				_state = ZombieState.WANDER;
	//			}
	//		}
	//		break;
	//	case(ZombieState.ESCAPING):
			
	//		break;
	//	}
	//}
	
	//// Update is called once per frame
	//void Update () {
	//	StateMachine();
	//}
}
