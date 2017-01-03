using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip runSound;
    public AudioClip jumpSound;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Space))
        {
                audioSource.PlayOneShot(jumpSound);
            
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(runSound);
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

	}
}
