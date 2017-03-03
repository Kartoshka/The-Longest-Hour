using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;
using Cinemachine.Attributes;
using Cinemachine.Blending;
using Cinemachine.Assets;
using Cinemachine;

public class SplineDive : MonoBehaviour {

	[SerializeField]
	private BezierSpline d_spline;

	public CinemachineVirtualCamera diveCam;

	public float duration = 3;
	[SerializeField]
	private float d_time = 0.0f;
	private bool diving = false;
	[Range(0,1)]
	public float lerpAmount =0.5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (diving) 
		{
			if (d_time <= duration)
			{
				
				Vector3 destination = d_spline.GetPoint ((d_time/duration));
				destination = Vector3.Lerp (destination, this.gameObject.transform.position, lerpAmount);
				this.gameObject.transform.position = destination;
				d_time += Time.deltaTime;
			} 
			else 
			{
				d_time = 0;
				if (diveCam != null) {
					diveCam.gameObject.SetActive (false);
				}
				diving = false;
			}

		}
		
	}


	void OnTriggerEnter(Collider c)
	{
		BezierSpline a = c.gameObject.GetComponent<BezierSpline> ();

		if (a != null && !diving) 
		{
			if (diveCam != null) {
				diveCam.gameObject.SetActive (true);
			}
			d_spline = a;
			diving = true;
			d_time = 0;
		}
	}
}
