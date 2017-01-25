using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {

	public float initialTime = 20*60;
	private float timeLeft =0;

	public Text display;

	void Start(){
		timeLeft = initialTime;
	}

	// Update is called once per frame
	void Update () {
		if (removeTime (GlobalTimeManager.getDeltaTime ())) {
			//Game Over
		}

		if (display != null) {
			display.text = formatTime (timeLeft);
		}
	}


	public bool removeTime(float seconds)
	{
		timeLeft = (timeLeft - seconds) > 0 ? timeLeft - seconds : 0;
		return timeLeft != 0;
	}

	public float getTimeLeft()
	{
		return timeLeft;
	}

	public static string formatTime(float timeInSeconds)
	{
		int minutes = Mathf.FloorToInt(timeInSeconds / 60);
		int seconds = (int)(timeInSeconds % 60);
		int millis = (int)((timeInSeconds - minutes * 60 - seconds) * 100);
		return minutes + ":" + seconds + ":" + millis;

	}
}
