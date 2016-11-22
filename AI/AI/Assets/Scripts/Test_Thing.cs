using UnityEngine;
using System.Collections;

public class Test_Thing : MonoBehaviour {


    private KBM_Input kbmInput;

	// Use this for initialization
	void Start () {
        kbmInput = new KBM_Input();
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(kbmInput.keyA_Down);

	}
}
