using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorEnd : MonoBehaviour {

    public Transform startPosForBall;
    public GameObject targetDoor;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("generatorBall"))
        {
            GeneratorBall gb = other.gameObject.GetComponent<GeneratorBall>();
            if(gb.chargeAmount == 100)
            {
                Destroy(targetDoor);
                Destroy(other.gameObject);
            }
            else
            {
                gb.transform.position = startPosForBall.position;
            }
        }
    }
}
