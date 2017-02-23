using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public CarSpawnerManager carSpawnerManger;

    void OnTriggerEnter(Collider other)
    {
       carSpawnerManger.spawnCar();
       Destroy(other.gameObject);
    }
}
