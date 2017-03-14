using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraRotator : MonoBehaviour {

    public float leftDisplacement;
    public float rightDisplacement;

    bool changed;
    int ticks;
    void Start()
    {
        changed = false;
        ticks = 0;
    }

	void Update()
    {
        if(!changed)
        {
            StartCoroutine(change());
        }
    }

    IEnumerator change()
    {
        changed = true;
        if((Random.Range(0, 10)%2) == 0)
        {
            if(ticks == 3)
            {
                transform.Rotate(0.0f, 0.0f, rightDisplacement);
                ticks--;
            }
            else
            {
                transform.Rotate(0.0f, 0.0f, leftDisplacement);
                ticks++;
            }
        }
        else
        {
            if(ticks == -3)
            {
                transform.Rotate(0.0f, 0.0f, leftDisplacement);
                ticks++;
            }
            else
            {
                transform.Rotate(0.0f, 0.0f, rightDisplacement);
                ticks--;
            }
        }
        yield return new WaitForSeconds(1.3f);
        changed = false;
    }
}
