using UnityEngine;
using System.Collections;

public class DontRotateBean : MonoBehaviour {
	// Update is called once per frame
	void Update () {

        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
}
