using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOnFall : MonoBehaviour {
    Vector3 m_initialPosition = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
        m_initialPosition = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("FallRespawner"))
        {
            gameObject.transform.position = m_initialPosition;
        }
    }
}
