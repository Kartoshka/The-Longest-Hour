using UnityEngine;
using System.Collections;

public class SpeedUp : PowerUpModifier {

    public override float getMaxAccelerationModifier()
    {
        return 10.0f;
    }

}
