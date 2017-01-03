using UnityEngine;
using System.Collections;

public class WaypointMover : MonoBehaviour {
    
    public Transform target;
    public float speed;

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
