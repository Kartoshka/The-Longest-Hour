using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlatformerController : MonoBehaviour {

    bool m_unlockedGrenades = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(m_unlockedGrenades && Input.GetKeyDown(KeyCode.Space))
        {
            ThrowGrenade();
        }
	}

    void ThrowGrenade()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Grenade"))
        {
            m_unlockedGrenades = true;
            Destroy(collision.gameObject);
        }
    }
}
