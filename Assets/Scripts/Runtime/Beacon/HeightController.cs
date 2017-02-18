using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class HeightController : MonoBehaviour {

	[SerializeField]
	private Transform a_Player;

	private RectTransform a_RectTransform;

	[SerializeField]
	private float a_DistanceBetween;

	[SerializeField]
	private Vector2 a_ScaledSize;

	[SerializeField]
	private Vector3 a_ScaledPosition;

	private Vector3 a_BeaconPosition;
	private Vector3 a_PlayerPosition;

	[SerializeField]
	private float a_MaxHeight;
	[SerializeField]
	private float a_MinHeight;

	//private float a_Epsilon;


	private void scaleHeight()
	{

		a_BeaconPosition = transform.position;
		a_PlayerPosition = a_Player.position;

		a_DistanceBetween = Vector3.Distance (a_BeaconPosition, a_PlayerPosition);
		a_ScaledSize.y = a_DistanceBetween;



		if (a_ScaledSize.y >= a_MaxHeight) { a_ScaledSize.y = a_MaxHeight; }

		if (a_ScaledSize.y <= a_MinHeight) { a_ScaledSize.y = a_MinHeight; }

		a_RectTransform.sizeDelta = a_ScaledSize;
		//a_ScaledPosition.y = 
		//a_RectTransform.localPosition = a_ScaledPosition;
		 
	}

	// Use this for initialization
	void Start () {
		a_RectTransform = GetComponent<RectTransform>();
		a_ScaledPosition = a_RectTransform.localPosition;
		a_ScaledSize.x = a_RectTransform.sizeDelta.x;

		a_MaxHeight = 40f;
		a_MinHeight = 9f;
	}
	
	// Update is called once per frame
	void Update () {

		scaleHeight();

	}
}
