using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerManager : MonoBehaviour {

    public GameObject source;
    public List<GameObject> cars;
    public int carsToSpawn;
    public bool isAffectedByTime;
    public float timeBetweenRewinds;

    bool isRewinding;

    List<GameObject> carsSpawned;

	// Use this for initialization
	void Start ()
    {
        isRewinding = false;
        for(int i = 0; i < carsToSpawn; i++)
        {
            spawnCar();
        }
	}

    void Update()
    {
        if(!isAffectedByTime)
        {
            return;
        }

        if(!isRewinding)
        {
            StartCoroutine(rewind());
        }
    }

    IEnumerator rewind()
    {
        isRewinding = true;
        yield return new WaitForSeconds(timeBetweenRewinds);
        foreach(GameObject car in carsSpawned)
        {
            car.transform.position = car.transform.position - car.transform.forward;
        }
        isRewinding = false;
    }
	
	
    public void spawnCar()
    {
        GameObject car = cars[Random.Range(0, cars.Count)];

        float x = source.transform.position.x + Random.Range(-15.0f, 15.0f);
        float y = source.transform.position.y + Random.Range(-20.0f, 20.0f);
        float z = source.transform.position.z + Random.Range(-20.0f, 20.0f);

        GameObject spawnedCar = Instantiate(car, new Vector3(x, y, z), transform.rotation);
        
        if(isAffectedByTime)
        {
            carsSpawned.Add(spawnedCar);
        }
    }
}
