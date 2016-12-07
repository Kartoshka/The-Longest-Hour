using UnityEngine;
using System.Collections;

public class WaypointMover : MonoBehaviour {

    private Rigidbody rigidBody;
    public Transform target;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate()
    {
        Vector3 targetDirection = Vector3.MoveTowards(transform.position, target.transform.position, 2.0f);
        transform.position = targetDirection * Time.deltaTime;
    }

    public void setTarget(Transform p_target)
    {
        this.target = p_target;
    }
}
