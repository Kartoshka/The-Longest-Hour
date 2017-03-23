using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IgnoreCollisions : MonoBehaviour {

    public LayerMask ignoreCollisionsOnLayer;
    Rigidbody rb;
    bool initialKinematic;
	// Use this for initialization
	void Start () {
        Collider collider = this.gameObject.GetComponent<Collider>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        initialKinematic = rb.isKinematic;
    }

     void OnCollisionEnter(Collision c)
    {
        
        if (ignoreCollisionsOnLayer==(ignoreCollisionsOnLayer | (1 << c.gameObject.layer)))
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            Physics.IgnoreCollision(this.gameObject.GetComponent<Collider>(), c.collider);
        }
    }

    private void OnCollisionExit(Collision c)
    {
        if (ignoreCollisionsOnLayer == (ignoreCollisionsOnLayer | (1 << c.gameObject.layer)))
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = initialKinematic;
 
        }
    }
}
