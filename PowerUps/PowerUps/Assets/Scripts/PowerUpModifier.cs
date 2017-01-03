using UnityEngine;
using System.Collections;

public abstract class PowerUpModifier : MonoBehaviour{
    
    public virtual float getMaxAccelerationModifier()
    {
        return 0;
    }

    public virtual float getJumpHeightModifier()
    {
        return 0;
    }
    
    public virtual float getMaxHealthModifier()
    {
        return 0;
    }

}
