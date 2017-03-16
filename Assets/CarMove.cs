using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour {

    public float timeUntilDeath;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(killMe());
	}
	
    IEnumerator killMe()
    {
        yield return new WaitForSeconds(timeUntilDeath);
        Destroy(gameObject);
    }
}
