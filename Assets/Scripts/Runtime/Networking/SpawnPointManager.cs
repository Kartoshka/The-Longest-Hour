using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class SpawnPointManager : NetworkBehaviour
{
	public Transform groundPlayerSpawnPosition;
	public Transform airPlayerStartPosition;

	public List<Transform> enemySpawnPositions = new List<Transform>();

	public List<Transform> allySpawnPositions = new List<Transform>();

	public Transform getPlayerSpawnTransform(int playerId)
	{
		Transform spawnTransform = null;
		switch(playerId)
		{
			case (0):
			{
				spawnTransform = groundPlayerSpawnPosition;
                break;
			}
			case (1):
			{
				spawnTransform = airPlayerStartPosition;
				break;
			}
		}
		return spawnTransform;
	}
}

