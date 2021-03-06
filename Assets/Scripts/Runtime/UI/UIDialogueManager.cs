﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueManager : MonoBehaviour {

	/// <summary>
	/// Animator of the dialogue panel
	/// </summary>
	private Animator animator;

	/// <summary>
	/// Icon showing Bear is currently speaking
	/// </summary>
	[SerializeField]
	private Image _BearIcon;

	/// <summary>
	/// Icon showing Bird is currently speaking
	/// </summary>
	[SerializeField]
	private Image _BirdIcon;

	/// <summary>
	/// Text placed in dialogue box
	/// </summary>
	[SerializeField]
	private Text _Text;

	/// <summary>
	/// Bool to ensure two separate dialogue scripts don't get played simultaneously
	/// Only when a script is fully read through can another script begin being read
	/// </summary>
	private bool isDisplayingDialogue;


	// Use this for initialization
	void Start () {

		// Get the Animator component
		animator = GetComponent<Animator> ();

		// Hide the Bear and Bird icons at start
		_BearIcon.gameObject.SetActive (false);
		_BirdIcon.gameObject.SetActive (false);

		// Hide dialogue box at the start
		hideDialogue ();
	}


	private void handleInput()
	{
		/*if (Input.GetKeyDown (KeyCode.Alpha1)) 
		{
			showDialogue(_TestLines);
		}*/

	}


	/// <summary>
	/// Method which calls the coroutine to display dialogue on screen
	/// </summary>
	/// <param name="p_Script">P script.</param>
	public void showDialogue(List<UIDialogueLine> p_Script)
	{
		// If a script has already started being read, 
		// do not allow it to be interrupted by another script
		if (isDisplayingDialogue) return;

		// Play 'ShowDialogue' animation to reveal the dialogue box
		animator.Play ("ShowDialogue");

		// Start coroutine to iterate through lines of dialogue and display them
		StartCoroutine (co_DisplayDialogue (p_Script));
	}


	/// <summary>
	/// Loops through list of dialogue lines, waiting 5 seconds between each line
	/// </summary>
	/// <returns>The display dialogue.</returns>
	/// <param name="p_DialogueList">P dialogue list.</param>
	private IEnumerator co_DisplayDialogue(List<UIDialogueLine> p_DialogueList)
	{
		// Set isDisplayingDialogue to true so no other script can interrupt this coroutine
		isDisplayingDialogue = true;

		// Loop through list of dialogue and display. Waiting 5 seconds between each line
		foreach (UIDialogueLine dialogue in p_DialogueList) 
		{
			_Text.text = dialogue.Value;
			setSpeakerTo (dialogue.Speaker);
			yield return new WaitForSeconds (3f);
		}
			
		// Once over, hide dialogue box and set speaker to None to disable the icon
		hideDialogue ();
		setSpeakerTo ("None");

		// Allow for another script to be read
		isDisplayingDialogue = false;

	}

	/// <summary>
	/// Play the HideDialogue animation to conceal dialogue box
	/// </summary>
	public void hideDialogue()
	{
		animator.Play ("HideDialogue");
	}

	/// <summary>
	/// Sets the speaker to the paramater p_Speaker
	/// </summary>
	/// <param name="p_Speaker">P speaker.</param>
	private void setSpeakerTo(string p_Speaker)
	{
		switch (p_Speaker) 
		{
		case "Bear":
			_BearIcon.gameObject.SetActive (true);
			_BirdIcon.gameObject.SetActive (false);
			break;
		case "Bird":
			_BearIcon.gameObject.SetActive (false);
			_BirdIcon.gameObject.SetActive (true);
			break;
		case "None":
			_BearIcon.gameObject.SetActive (false);
			_BirdIcon.gameObject.SetActive (false);
			break;
		default:
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		handleInput ();
	}
}
