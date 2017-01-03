using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttributes : MonoBehaviour {

    private float maxAcceleration;
    private float maxJumpHeight;
    private float maxHealth;

    private Dictionary<int, PowerUpModifier>  powerUpModifiers;

    private void Start()
    {
        maxAcceleration = 1.0f;
        maxJumpHeight = 1.0f;
        maxHealth = 1.0f;

        powerUpModifiers = new Dictionary<int, PowerUpModifier>();
    }

    public float getMaxAcceleration()
    {
        return maxAcceleration + getMaxAccelerationModifier();
    }

    public float getMaxJumpHeight()
    {
        return maxJumpHeight + getMaxJumpHeightModifier();
    }

    public float getMaxHealth()
    {
        return maxHealth + getMaxHealthModifier();
    }


    public float getMaxAccelerationModifier()
    {
        float total = 0.0f;

        foreach(KeyValuePair<int, PowerUpModifier> powerUp in powerUpModifiers)
        {
            total = total + powerUp.Value.getMaxAccelerationModifier();
        }

        return total;
    }

    public float getMaxJumpHeightModifier()
    {
        float total = 0.0f;

        foreach (KeyValuePair<int, PowerUpModifier> powerUp in powerUpModifiers)
        {
            total = total + powerUp.Value.getJumpHeightModifier();
        }

        return total;
    }

    public float getMaxHealthModifier()
    {
        float total = 0.0f;

        foreach (KeyValuePair<int, PowerUpModifier> powerUp in powerUpModifiers)
        {
            total = total + powerUp.Value.getMaxHealthModifier();
        }

        return total;
    }


    public void putPowerUpModifier(PowerUpModifier powerUp)
    {
        int id = Random.Range(0, 50);
        powerUpModifiers.Add(id, powerUp);
        StartCoroutine(removePowerUp(id));
    }

    private IEnumerator removePowerUp(int id)
    {
        yield return new WaitForSeconds(3);
        powerUpModifiers.Remove(id);
    }
}
