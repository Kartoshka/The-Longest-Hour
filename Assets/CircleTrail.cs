using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTrail : MonoBehaviour {

    public float r;
    public float g;
    public float b;
    public float a;

    public void Start()
    {
        updateColor();
    }

	public void updateColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(r, g, b, a));
    }
}
