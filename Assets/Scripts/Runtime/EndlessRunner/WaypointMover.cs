using UnityEngine;
using System.Collections;

public class WaypointMover : MonoBehaviour {

    private Rigidbody rigidBody;
    public Transform target;
    public float speed;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, step);
    }

    public void setTarget(Transform p_target)
    {
        this.target = p_target;
    }
}
