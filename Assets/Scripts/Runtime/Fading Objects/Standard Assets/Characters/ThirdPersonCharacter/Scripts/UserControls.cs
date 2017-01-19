using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using FadingObjects;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	public class UserControls : TimeBasedObjects
    {
        public ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		private float h = 0;
		private float v = 0;
		private bool crouch;

       
        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {

			if (m_Character != null) {
				// calculate move direction to pass to character
				if (m_Cam != null) {
					// calculate camera relative direction to move:
					m_CamForward = Vector3.Scale (m_Cam.forward, new Vector3 (1, 0, 1)).normalized;
					m_Move = v * m_CamForward + h * m_Cam.right;
				} else {
					// we use world-relative directions in the case of no main camera
					m_Move = v * Vector3.forward + h * Vector3.right;
				}

				// pass all parameters to the character control script
				m_Character.Move (m_Move, crouch, m_Jump);
				m_Jump = false;
			}
        }
			
		protected override void OnPause ()
		{
			h = 0;
			v = 0;
		}

		protected override void OnResume ()
		{
		}

		protected override void PausedUpdate ()
		{
			if (Input.GetButtonDown ("PauseTime")) {
				GlobalTimeManager.getInstance ().Resume ();
				return;
			}

		}

		protected override void RunningUpdate ()
		{
			if (Input.GetButtonDown ("PauseTime")) {
				GlobalTimeManager.getInstance ().Pause ();

				return;
			}

			if (!m_Jump)
			{
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}

			// read inputs
			h = CrossPlatformInputManager.GetAxis("Horizontal");
			v = CrossPlatformInputManager.GetAxis("Vertical");
			crouch = Input.GetKey(KeyCode.C);
		}
    }
}
