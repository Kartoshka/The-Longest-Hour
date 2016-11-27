using UnityEngine;
using System.Collections;

public class SuperUp : PowerUpModifier {

    public override float getMaxAccelerationModifier()
    {
        return 10.0f;
    }

    public override float getJumpHeightModifier()
    {
        return 10.0f;
    }

    public override float getMaxHealthModifier()
    {
        return 1.0f;
    }
}
