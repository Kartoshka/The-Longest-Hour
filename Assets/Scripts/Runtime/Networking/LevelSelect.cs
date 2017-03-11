using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
	public List<ReadyIndicator> m_readyIndicators;

	public int m_nextSceneId = 0;
	public List<string> m_sceneNames;

	public void setNextSceneId(int sceneId)
	{
		if(sceneId < m_sceneNames.Count)
		{
			m_nextSceneId = sceneId;
		}
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool isAllReady = true;
		foreach(ReadyIndicator indicator in m_readyIndicators)
		{
			if(!indicator.getIsReady())
			{
				isAllReady = false;
				break;
            }
		}
		if(isAllReady)
		{
			//NetworkServer.SetAllClientsNotReady();
			NetworkManager.singleton.ServerChangeScene(m_sceneNames[m_nextSceneId]);
			//SceneManager.LoadScene(m_sceneNames[m_nextSceneId]);
		}
	}
}
