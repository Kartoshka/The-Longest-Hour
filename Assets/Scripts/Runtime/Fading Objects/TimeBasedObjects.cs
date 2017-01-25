using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FadingObjects{

	public abstract class TimeBasedObjects : MonoBehaviour {

		public string name;
		private bool paused = false;

		void Start()
		{
			GlobalTimeManager.registerTimedObject (this);
			initialize ();
		}
		// Update is called once per frame
		void Update () {

			if (paused) {
				PausedUpdate ();
			} 
			else {
				RunningUpdate ();
			}
		}

		public void Pause()
		{
			if (!paused) {
				paused = true;
				OnPause ();
			}
		}

		public void Resume()
		{
			if (paused) {
				paused = false;
				OnResume ();
			}
		}

		protected abstract void initialize();

		protected abstract void OnPause ();

		protected abstract void OnResume();

		protected abstract void PausedUpdate ();

		protected abstract void RunningUpdate();

	}
}
