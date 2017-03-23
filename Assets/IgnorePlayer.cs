using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePlayer : MonoBehaviour {

    bool doOnce;

	void Start()
    {
        doOnce = false;
    }

    void LateUpdate()
    {
        //Ignore player collisions
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            foreach (Collider coll in player.GetComponentsInChildren<Collider>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), coll);
            }
        }

        //Ignore car collisions
        foreach (GameObject car in GameObject.FindGameObjectsWithTag("Car"))
        {
            foreach (Collider coll in car.GetComponentsInChildren<Collider>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), coll);
            }
        }
    }
}
