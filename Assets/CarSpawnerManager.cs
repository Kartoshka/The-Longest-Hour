using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerManager : MonoBehaviour {

    public GameObject source;
    public List<GameObject> cars;
    public int carsToSpawn;
    public bool isAffectedByTime;
    public float timeBetweenRewinds;
    public float amountToReverse;
    bool isRewinding;
    bool rewinded;
    List<GameObject> carsSpawned;

	// Use this for initialization
	void Start ()
    {
        isRewinding = false;
        rewinded = false;
        carsSpawned = new List<GameObject>();

        for(int i = 0; i < carsToSpawn; i++)
        {
            spawnCar();
        }
	}

    //void Update()
    //{
    //    if(!isAffectedByTime)
    //    {
    //        return;
    //    }

    //    if (!isRewinding)
    //    {
    //        if(rewinded)
    //        {
    //            StartCoroutine(resume());
    //        }
    //        else
    //        {
    //            StartCoroutine(rewind());
    //        }
    //    }
    //}

    //Every body two step back
    IEnumerator rewind()
    {
        isRewinding = true;
        yield return new WaitForSeconds(timeBetweenRewinds);
        foreach(GameObject car in carsSpawned)
        {
            if(car != null)
            {
                continue;
            }

            LinearInputMoverBehavior limb = car.GetComponent<LinearInputMoverBehavior>();

            if (limb.getForwardStepSize().x > 0)
            {
                limb.setForwardStepSize(limb.getForwardStepSize() * -0.5f);
            }
        }
        rewinded = true;
        isRewinding = false;
    }
	
    //Every body two step forward
    IEnumerator resume()
    {
        isRewinding = true;
        yield return new WaitForSeconds(timeBetweenRewinds);
        foreach(GameObject car in carsSpawned)
        {
            if(car == null)
            {
                continue;
            }

            LinearInputMoverBehavior limb = car.GetComponent<LinearInputMoverBehavior>();

            if (limb.getForwardStepSize().x < 0)
            {
                limb.setForwardStepSize(limb.getForwardStepSize() * -2f);
            }
        }
        rewinded = false;
        isRewinding = false;
    }
	
    public void spawnCar()
    {
        GameObject car = cars[Random.Range(0, cars.Count)];

        float x = source.transform.position.x + Random.Range(-15.0f, 15.0f);
        float y = source.transform.position.y + Random.Range(-20.0f, 20.0f);
        float z = source.transform.position.z + Random.Range(-20.0f, 20.0f);

        GameObject spawnedCar = Instantiate<GameObject>(car, new Vector3(x, y, z), transform.rotation);
        
        if(isAffectedByTime)
        {
            carsSpawned.Add(spawnedCar);
        }
    }
}
