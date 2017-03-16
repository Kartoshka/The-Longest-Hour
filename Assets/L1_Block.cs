using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_Block : MonoBehaviour {

    public float transitionTime;
    public float heightToTransitionTo;

    bool movedUp;
    bool transitioning;
	// Update is called once per frame
	void Update ()
    {
		if(!transitioning)
        {
            StartCoroutine(transition());
        }
	}

    IEnumerator transition()
    {
        transitioning = true;
        yield return new WaitForSeconds(transitionTime);
        if(movedUp)
        {
            movedUp = false;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - heightToTransitionTo, gameObject.transform.position.z);
        }
        else
        {
            movedUp = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + heightToTransitionTo, gameObject.transform.position.z);
        }
        transitioning = false;
    }
}
