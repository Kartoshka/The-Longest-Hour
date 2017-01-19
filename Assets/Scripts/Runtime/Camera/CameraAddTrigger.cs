using UnityEngine;
using System.Collections;

public class CameraAddTrigger : MonoBehaviour {

	public CameraStackManager camMan;
	public AbCamera cameraToAdd;
	public float blendInTime;

	GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider target)
	{
		if (target.gameObject.tag == "Player") {
			camMan.addGameCam (((GameObject)Instantiate(cameraToAdd.gameObject)).GetComponent<AbCamera>(), blendInTime);
		}
	}
}
