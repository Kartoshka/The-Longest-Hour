using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public CarSpawnerManager carSpawnerManger;

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag.Equals("Car"))
        {
            carSpawnerManger.spawnCar();
            Destroy(gameObject);
        }
    }
}
