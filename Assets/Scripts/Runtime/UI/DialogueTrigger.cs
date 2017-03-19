using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    
    public List<string> bearLines;
    public List<string> birdLines;

    List<UIDialogueLine> lines;
    UIManager uiManager;
	// Use this for initialization
	void Start ()
    {
        uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        lines = new List<UIDialogueLine>();

        for(int i = 0; i < bearLines.Count; i++)
        {
            lines.Add(new UIDialogueLine("Bear", bearLines[i]));
            lines.Add(new UIDialogueLine("Bird", birdLines[i]));
        }
	}
	
	void OnTriggerEnter(Collider other)
    {
        //uiManager.submitLines(lines);
        //Destroy(gameObject);
    }
}
