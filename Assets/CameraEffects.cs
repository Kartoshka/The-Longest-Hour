using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour {

    Camera main;

    void Start()
    {
        main = Camera.main;
        main.GetComponent<Twirl>();
    }

	void fadeIn()
    {

    }

    void fadeOut()
    {

    }
}
