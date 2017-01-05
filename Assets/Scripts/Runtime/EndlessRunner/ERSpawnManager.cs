using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// </summary>
public class ERSpawnManager : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////   

	public GameObject[] m_groundTilePrefabs;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes - These will either be created by this class or set by a another.
	//////////////////////////////////////////////////////////////////////////////////////////

	public int m_maxTileCount = 5;
	protected LinkedList<GameObject> m_spawnedTiles = new LinkedList<GameObject>();

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	public void spawnNextTile(Vector3 position, Quaternion rotation)
	{
		if(m_groundTilePrefabs.Length > 0)
		{
            int prefabIndex = Random.Range(0, m_groundTilePrefabs.Length - 1);
			GameObject prefab = m_groundTilePrefabs[prefabIndex];
			GameObject spawnedObject = (GameObject)Instantiate(prefab, position, prefab.transform.localRotation);
			m_spawnedTiles.AddLast(spawnedObject);
			if(m_spawnedTiles.Count > m_maxTileCount)
			{
				GameObject expiredObject = m_spawnedTiles.First.Value;
				m_spawnedTiles.RemoveFirst();
				Destroy(expiredObject);
            }
        }
	}

    public void spawnNextTile(GameObject objectToSpawn, Vector3 position)
    {
        if (m_groundTilePrefabs.Length > 0)
        {
            GameObject spawnedObject = (GameObject)Instantiate(objectToSpawn, position, objectToSpawn.transform.localRotation);
            m_spawnedTiles.AddLast(spawnedObject);
            if (m_spawnedTiles.Count > m_maxTileCount)
            {
                GameObject expiredObject = m_spawnedTiles.First.Value;
                m_spawnedTiles.RemoveFirst();
                Destroy(expiredObject);
            }
        }
    }


    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////
    #region Runtime
    //////////////////////////////////////////////////////////////////////////////////////////  

    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////
    #region Persistence
    //////////////////////////////////////////////////////////////////////////////////////////

    #endregion
}