using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FadingObjects;

public class GlobalTimeManager : TimeBasedObjects {


	private static float lastUpdateTime = 0;
	public IList<TimeBasedObjects> activeObjects;
	private static GlobalTimeManager singleton;

	public GlobalCameras globalCam;
		
	public static GlobalTimeManager getInstance()
	{
		if (singleton == null) {

			GameObject globalHolder = new GameObject ("GlobalManagers");
			globalHolder.tag = "Global";
			singleton = globalHolder.AddComponent<GlobalTimeManager> ();
			singleton.activeObjects = new List<TimeBasedObjects> ();

		} 

		return singleton;
	}

	public static void registerTimedObject(TimeBasedObjects tObject)
	{
		if (tObject != getInstance()) {
			getInstance ().activeObjects.Add (tObject);
		}
	}
	
	protected override void OnPause ()
	{
		Debug.Log (activeObjects.Count);
		IEnumerator<TimeBasedObjects> listEnumerator = activeObjects.GetEnumerator ();
		while(listEnumerator.MoveNext()) {
			TimeBasedObjects current = listEnumerator.Current;
			if (current != null) {
				current.Pause ();
			} else {
				activeObjects.Remove (current);
			}
		}
		Time.timeScale = 0f;

		if (globalCam != null) {
			globalCam.switchToFreeCamera ();
		}
	}

	protected override void OnResume ()
	{
		IEnumerator<TimeBasedObjects> listEnumerator = activeObjects.GetEnumerator ();
		while(listEnumerator.MoveNext()) {
			TimeBasedObjects current = listEnumerator.Current;
			if (current != null) {
				current.Resume ();
			} else {
				activeObjects.Remove (current);
			}
		}

		if (globalCam != null) {
			globalCam.switchToPlayer ();
		}

		Time.timeScale = 1.0f;
	}


	public static float getDeltaTime()
	{
		float current = Time.time;
		float delta = current - lastUpdateTime;
		lastUpdateTime= current;
		return delta;
	}
	protected override void PausedUpdate ()
	{
	
	}

	protected override void RunningUpdate ()
	{
	}


}
