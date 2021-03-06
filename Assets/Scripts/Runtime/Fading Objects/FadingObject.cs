﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FadingObjects;

[RequireComponent(typeof(MeshRenderer))]
public class FadingObject : TimeBasedObjects {


	private Material mat;
	private float initialFade =1;

	public float duration = 0;
	public float accumulatedTime = 0;

	protected override void initialize()
	{
		mat = this.gameObject.GetComponent<MeshRenderer> ().material;
		initialFade = mat.GetFloat ("_AlphaValue");
	}

	protected override void OnPause ()
	{
		
	}

	protected override void OnResume ()
	{

	}

	protected override void PausedUpdate ()
	{
	
	}

	protected override void RunningUpdate ()
	{
		accumulatedTime += Time.deltaTime;
		float fade = (duration - accumulatedTime)*initialFade / duration;
		mat.SetFloat ("_AlphaValue", fade);
			
		if (accumulatedTime >= duration) {
			Destroy (this.gameObject);
		}
	}
}
