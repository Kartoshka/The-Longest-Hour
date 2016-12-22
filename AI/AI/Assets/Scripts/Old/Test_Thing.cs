using UnityEngine;
using System.Collections;

public class Test_Thing : MonoBehaviour {


    private KBM_Input kbmInput;
   
	void Start()
    {
        kbmInput = gameObject.GetComponent<KBM_Input>();
    }

	// Update is called once per frame
	void Update () {

        Debug.Log(kbmInput.keyA_Hold);

	}
}
