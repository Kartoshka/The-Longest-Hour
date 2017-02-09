using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagMany : MonoBehaviour {

    SphereCollider myCollider;
    public float expansion;
    public float lifetime;

    void Start()
    {
        myCollider = gameObject.GetComponent<SphereCollider>();
        StartCoroutine(blownUp());
    }

    void Update()
    {
        myCollider.radius = myCollider.radius + expansion * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TimeControllable")
        {
            other.gameObject.GetComponent<TimeControllable>().enabled = true;
            other.gameObject.GetComponent<TimeControllable>().Activate();
        }
    }

    IEnumerator blownUp()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    
}
