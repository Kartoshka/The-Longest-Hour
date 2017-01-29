using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTrail : MonoBehaviour {

    public Material radiusMaterial;
    public float radius = 2.5f;
    public Color color = Color.red;

    
	// Update is called once per frame
	void Update () {

        radiusMaterial.SetVector("_Center",  this.transform.position);
        radiusMaterial.SetFloat("_Radius", this.radius);
        radiusMaterial.SetColor("_RadiusColor", this.color);

	}
}
