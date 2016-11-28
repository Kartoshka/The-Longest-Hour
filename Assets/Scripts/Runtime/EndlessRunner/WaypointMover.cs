using UnityEngine;
using System.Collections;

public class WaypointMover : MonoBehaviour {

    private Rigidbody rigidBody;
    public GameObject target;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate()
    {
        Vector3 targetDirection = Vector3.MoveTowards(transform.position, target.transform.position, 1.0f);
        rigidBody.MovePosition(transform.position + targetDirection * Time.deltaTime);
        transform.Rotate(new Vector3(20.0f, 20.0f, 20.0f) * Time.deltaTime);
    }


	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Waypoint"))
        {
            target = other.gameObject.GetComponent<Waypoint>().target;
        }
    }
}
