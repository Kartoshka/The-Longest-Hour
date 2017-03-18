using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UILobbyManager : MonoBehaviour {

	/// <summary>
	/// Bool to wait one frame before Prompt starts listening for input
	/// </summary>
	private bool _frameWait;

	#region Player Ready attributes

	/// <summary>
	/// Bool stating Bear Player is ready to play
	/// </summary>
	private bool _isBearReady;

	/// <summary>
	/// Bool showing Bird player is ready to play
	/// </summary>
	private bool _isBirdReady;


	/// <summary>
	/// Image displaying the Bear has hit the prompt
	/// </summary>
	[SerializeField]
	private Image _BearReadyImage;

	/// <summary>
	/// Image displaying the Bird has hit the prompt
	/// </summary>
	[SerializeField]
	private Image _BirdReadyImage;

	/// <summary>
	/// The bird trail image.
	/// </summary>
	[SerializeField]
	private Image _BirdTrailImage;

	#endregion

	#region Prompt Button Attributes
	/// <summary>
	/// Button prompting player to press the A button
	/// </summary>
	[SerializeField]
	private Button _PromptButtonA;

	/// <summary>
	/// The animator attached to the PromptButton gameObject
	/// </summary>
	private Animator _PromptAnimator;

	/// <summary>
	/// RuntimeAnimatorController needed to get length of animation
	/// </summary>
	private RuntimeAnimatorController _RuntimeAC;

	/// <summary>
	/// Float holding length of the 'pressed' animation of the button
	/// </summary>
	private float _PressedAnimationLength;

	/// <summary>
	/// Bool keeping track of if the player has pressed the prompted button and confirmed their presence
	/// </summary>
	private bool _hasPressed;

	/// <summary>
	/// Bool keeping track of coroutine calls to ensure they are contacted only once
	/// </summary>
	private bool _isDisplaying;

	#endregion

	#region Game Logo Attributes

	/// <summary>
	/// Image for 'The' in game logo
	/// </summary>
	[SerializeField]
	private Image _ImageThe;

	/// <summary>
	/// Image for 'Longest' in game logo
	/// </summary>
	[SerializeField]
	private Image _ImageLongest;

	/// <summary>
	/// Image for 'Hour' in game logo
	/// </summary>
	[SerializeField]
	private Image _ImageHour;

	#endregion

	// Use this for initialization
	void Start () {

		//startLobbyScreen ();

	}

	/// <summary>
	/// Instantiates Lobby screen values, by getting Animator information
	/// and turning all images but prompt to inactive
	/// </summary>
	public void startLobbyScreen()
	{
		// Set all animation values for the Pressed animation for smooth transitioning
		_PromptAnimator = _PromptButtonA.GetComponent<Animator> ();
		_RuntimeAC = _PromptAnimator.runtimeAnimatorController;

		// Get the animation length. Buffer by a slight offset.
		_PressedAnimationLength = _RuntimeAC.animationClips [2].length;
		_PressedAnimationLength += 0.1f;

		// Handle boolean start values
		_isDisplaying = false;
		_hasPressed = false;

		// Handle boolean keeping track of if a player is ready
		_isBearReady = false;
		_isBirdReady = false;

		// Bool to make input listen for input after a frame has passed
		_frameWait = true;

		// Set the right objects to active/inactice
		_PromptButtonA.gameObject.SetActive(true);
		_PromptAnimator.Play ("Normal");


		_BearReadyImage.gameObject.SetActive (false);
		_BirdReadyImage.gameObject.SetActive (false);
		_BirdTrailImage.gameObject.SetActive (false);
		_ImageThe.gameObject.SetActive (false);
		_ImageLongest.gameObject.SetActive (false);
		_ImageHour.gameObject.SetActive (false);
		
	}

	/// <summary>
	/// Method handling player input. In particular, pressing the prompted button
	/// </summary>
	private void handleInput()
	{

		// Buffer check, 
		// makes sure input doesn't receive a lingering 'submit' press from a previous frame into current frame
		// namely, when going from the Pause menu back to Lobby
		if (Input.GetAxisRaw ("Submit") <= 0) _frameWait = false;

		if (_frameWait) return;

		if (Input.GetAxisRaw ("Submit") > 0 && !_hasPressed) 
		{
			_PromptAnimator.Play ("Pressed");
			Debug.Log("Submit val: " + Input.GetAxisRaw ("Submit") );
			_hasPressed = true;

		}
	}

	/// <summary>
	/// Method updating display based on current player and buttons pressed
	/// </summary>
	private void handleDisplay()
	{

		// If prompted button was pressed, and coroutine has not been called yet
		if (_hasPressed && !_isDisplaying) 
		{
			StartCoroutine ("co_DisplayPlayerReadyImage");
			_isDisplaying = true;
		}

		// If both players have pressed prompted button, call the process to start the game
		if (_isBearReady && _isBirdReady) 
		{
			StartCoroutine ("co_StartGame");
		}
	}

	/// <summary>
	/// Display BirdReady image.
	/// </summary>
	private void showBearReady()
	{
		_BearReadyImage.gameObject.SetActive(true);
		_isBearReady = true;
	}


	/// <summary>
	/// Reveals the BirdReady Image, first the trail and then the bird image
	/// </summary>
	/// <returns>The image coroutine.</returns>
	private IEnumerator co_ShowBirdReady()
	{
		_BirdTrailImage.gameObject.SetActive(true);

		for (float f = 0; f <= 1; f+= 0.1f) 
		{
			_BirdTrailImage.fillAmount = f;
			yield return new WaitForSeconds (0.01f);
		}

		yield return new WaitForSeconds (0.02f);
		_BirdReadyImage.gameObject.SetActive (true);
		_isBirdReady = true;
	}


	/// <summary>
	/// Displays the player ready image after the prompt 'pressed' image has completed
	/// </summary>
	/// <returns>The player ready image.</returns>
	private IEnumerator co_DisplayPlayerReadyImage()
	{
		yield return new WaitForSeconds (_PressedAnimationLength);

		//TODO: Make so calls this method if Bird Player
		StartCoroutine("co_ShowBirdReady");

		//TODO: Make so calls this method if Bear Player
		showBearReady ();
	}
		
		


	/// <summary>
	/// 'Starts' the Game 
	/// (shows game logo then hides this Lobby panel and allows actual game to be seen)
	/// </summary>
	private IEnumerator co_StartGame()
	{
		yield return new WaitForSeconds (1f);
		_ImageThe.gameObject.SetActive (true);

		yield return new WaitForSeconds (1.1f);
		_ImageLongest.gameObject.SetActive (true);

		yield return new WaitForSeconds (1f);
		_ImageHour.gameObject.SetActive (true);

		yield return new WaitForSeconds (2.5f);
		UIManager.canPause = true;
		gameObject.SetActive (false);
		
	}

	// Update is called once per frame
	void Update () {

		handleInput ();
		handleDisplay ();
	}
}
