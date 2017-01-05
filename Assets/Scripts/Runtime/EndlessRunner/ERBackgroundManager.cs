using UnityEngine;
using System.Collections;

public class ERBackgroundManager : ERSpawnManager {

    public int collidersToSkip;
    public int collidersEntered;

    public void spawnBGTile(GameObject bgSpawnPosition)
    {
        Vector3 position = bgSpawnPosition.transform.position;
        collidersEntered++;
        if (m_groundTilePrefabs.Length > 0 && (collidersEntered > collidersToSkip))
        {
            int prefabIndex = Random.Range(0, m_groundTilePrefabs.Length - 1);
            GameObject prefab = m_groundTilePrefabs[prefabIndex];
            GameObject spawnedObject = (GameObject)Instantiate(prefab, position, prefab.transform.localRotation);
            m_spawnedTiles.AddLast(spawnedObject);
            if (m_spawnedTiles.Count > m_maxTileCount)
            {
                GameObject expiredObject = m_spawnedTiles.First.Value;
                m_spawnedTiles.RemoveFirst();
                Destroy(expiredObject);
            }
            bgSpawnPosition.transform.position = new Vector3(bgSpawnPosition.transform.position.x + spawnedObject.GetComponent<Renderer>().bounds.size.x, bgSpawnPosition.transform.position.y, bgSpawnPosition.transform.position.z);
            collidersEntered = 0;
        }
    }
}
