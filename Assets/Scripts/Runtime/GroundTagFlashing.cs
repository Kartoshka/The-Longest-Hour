using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTagFlashing : MonoBehaviour
{

    public float lowVal;
    public float highVal;
    public float stepAmount;

    private MeshRenderer meshRenderer;
    private float targetVal;
    private bool makeBrighter;

    // Use this for initialization
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        targetVal = highVal;
        makeBrighter = true;
    }

    void Update()
    {
        float currentEdging = meshRenderer.material.GetFloat("_Edging");
        if (makeBrighter)
        {
            float newVal = currentEdging + (stepAmount * Time.deltaTime);
            meshRenderer.material.SetFloat("_Edging", newVal);
            if (newVal > targetVal)
            {
                makeBrighter = false;
                targetVal = lowVal;
            }
        }
        else
        {
            float newVal = currentEdging - (stepAmount * Time.deltaTime);
            meshRenderer.material.SetFloat("_Edging", newVal);
            if (newVal < targetVal)
            {
                makeBrighter = true;
                targetVal = highVal;
            }
        }
    }

}
