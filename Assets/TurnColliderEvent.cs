using UnityEngine;
using System.Collections;

public class TurnColliderEvent : MonoBehaviour {

    public float turnAmount;

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.Rotate(Vector3.up, turnAmount);
        gameObject.SetActive(false);
    }
}
