using UnityEngine;
using System.Collections;

public class HealthUp : PowerUpModifier {

    public override float getMaxHealthModifier()
    {
        return 1.0f;
    }
}
