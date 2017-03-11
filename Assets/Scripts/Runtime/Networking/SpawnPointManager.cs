using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnPointManager : MonoBehaviour
{
	public Transform groundPlayerSpawnPosition;
	public Transform airPlayerStartPosition;

	public List<Transform> enemySpawnPositions = new List<Transform>();

	public List<Transform> allySpawnPositions = new List<Transform>();

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}

