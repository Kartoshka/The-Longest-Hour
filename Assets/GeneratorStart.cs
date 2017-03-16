using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorStart : MonoBehaviour {

    public Transform donePosition;
    public bool entered;
    GeneratorBall contained;

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("generatorBall"))
        {
            entered = true;
            GeneratorBall gb = other.gameObject.GetComponent<GeneratorBall>();
            contained = gb;
            if(gb.chargeAmount == 100)
            {
                other.gameObject.transform.position = donePosition.position;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag.Equals("generatorBall"))
        {
            entered = false;
        }
    }
}
