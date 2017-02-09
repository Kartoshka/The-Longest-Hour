using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTag : MonoBehaviour {

    public float lowVal;
    public float highVal;
    public float stepAmount;

    private Light myLight;
    private float targetVal;
    private bool makeBrighter;

	// Use this for initialization
	void Start ()
    {
        myLight = GetComponent<Light>();
        targetVal = highVal;
        makeBrighter = true;
	}

    void Update()
    {
        if(makeBrighter)
        {
            myLight.range = myLight.range + (stepAmount * Time.deltaTime);
            if (myLight.range > targetVal)
            {
                makeBrighter = false;
                targetVal = lowVal;
            }
        }
        else
        {
            myLight.range = myLight.range - (stepAmount * Time.deltaTime);
            if (myLight.range < targetVal)
            {
                makeBrighter = true;
                targetVal = highVal;
            }
        }
    }
	
}
