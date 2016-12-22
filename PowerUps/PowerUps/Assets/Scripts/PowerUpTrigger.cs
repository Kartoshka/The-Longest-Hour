using UnityEngine;
using System.Collections;

public class PowerUpTrigger : MonoBehaviour {

    public GameObject powerUp;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerAttributes>().putPowerUpModifier(powerUp.GetComponent<PowerUpModifier>());
            Destroy(this.gameObject);
        }
    }
}
