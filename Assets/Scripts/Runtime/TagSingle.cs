using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSingle : MonoBehaviour
{
    
    bool isInTaggableTrigger;
    GameObject tagTarget;

	// Use this for initialization
	void Start ()
    {
        isInTaggableTrigger = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Fire1"))
        {
            if(isInTaggableTrigger)
            {
                taggify();
            }
        }
	}

   

    void taggify()
    {
        if(tagTarget.GetComponent<TimeControllable>().enabled)
        {
            unTaggify();
            return;
        }
        tagTarget.GetComponent<TimeControllable>().enabled = true;
        tagTarget.GetComponent<TimeControllable>().Activate();
    }

    void unTaggify()
    {
        tagTarget.GetComponent<TimeControllable>().Deactivate();
        tagTarget.GetComponent<TimeControllable>().enabled = false;
    }
}
