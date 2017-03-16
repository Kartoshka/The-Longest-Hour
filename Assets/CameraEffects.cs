using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffects : MonoBehaviour {

    Camera main;
    Twirl twirl;
    void Start()
    {
        main = Camera.main;
        twirl = main.GetComponent<Twirl>();
    }

	public void transition()
    {
       while(twirl.angle < 270f)
        {
            twirl.angle++;
        }
       
       while(twirl.angle > 0)
        {
            twirl.angle--;
        }
    }
    
}
