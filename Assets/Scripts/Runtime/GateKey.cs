using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKey : MonoBehaviour {

    public int key;

    GateState gateState;

    void Start()
    {
        gateState = GetComponentInParent<GateState>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            gateState.disableLock(key);
            Destroy(gameObject);
        }
    }
}
