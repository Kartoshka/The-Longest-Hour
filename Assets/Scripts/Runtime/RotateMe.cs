using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour {

    public float rotateAmount;
    
	void Update ()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateAmount);
	}

    
}
