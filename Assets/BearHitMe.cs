using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearHitMe : MonoBehaviour {

	void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name.Equals("Attack Collider"))
        {
            Destroy(gameObject);
        }
    }
}
