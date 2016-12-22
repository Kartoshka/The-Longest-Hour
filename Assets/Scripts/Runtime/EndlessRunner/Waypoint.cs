using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    public Transform target;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("W_Mover"))
        {
            other.gameObject.GetComponent<WaypointMover>().setTarget(target);
            Debug.Log("big money salvia here");
        }
    }

}
