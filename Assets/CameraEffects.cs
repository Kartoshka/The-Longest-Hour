using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraEffects : MonoBehaviour {

    public float forwardSpeed;
    public float reverseSpeed;

    Camera main;
    Twirl twirl;

    bool increase;
    bool decrease;

    void Start()
    {
        main = Camera.main;
        twirl = main.GetComponent<Twirl>();
        increase = false;
        decrease = false;
    }
    
    void Update()
    {
        if(increase)
        {
            twirl.angle += forwardSpeed;
            if(twirl.angle == 270)
            {
                increase = false;
                decrease = true;
            }
        }
        else if(decrease)
        {
            twirl.angle -= reverseSpeed;
            if(twirl.angle == 0)
            {
                decrease = false;
            }
        }
    }

    public void transition()
    {
        increase = true;
    }
}
