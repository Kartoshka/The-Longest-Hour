using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerManager : MonoBehaviour {

    public GameObject source;
    public List<GameObject> cars;
    public int carsToSpawn;

	// Use this for initialization
	void Start ()
    {
        for(int i = 0; i < carsToSpawn; i++)
        {
            spawnCar();
        }
	}
	
	
    public void spawnCar()
    {
        GameObject car = cars[Random.Range(0, cars.Count)];

        float x = source.transform.position.x + Random.Range(-15.0f, 15.0f);
        float y = source.transform.position.y + Random.Range(-20.0f, 20.0f);
        float z = source.transform.position.z + Random.Range(-20.0f, 20.0f);

        Instantiate(car, new Vector3(x, y, z), transform.rotation);
        
    }
}
