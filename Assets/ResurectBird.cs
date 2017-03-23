using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurectBird : MonoBehaviour {

    public FollowTarget reference;
    public MoverComponent watchOverMe; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!watchOverMe)
        {
            GameObject birdClone = GameObject.Find("AirDrawer(Clone)");
            if (birdClone)
            {
               GameObject parent= birdClone.transform.root.gameObject;
                CustomNetworkedPlayer saver = parent.GetComponent<CustomNetworkedPlayer>();
                if (saver)
                {
                    saver.resetPlayer(reference.gameObject);
                }
            }
            //GameObject birdClone = GameObject.Find("AirDrawer(Clone)");
            //GameObject[] players =GameObject.FindGameObjectsWithTag("Player");
            //foreach(GameObject p in players)
            //{
            //    if()
            //}

        }
	}
}
