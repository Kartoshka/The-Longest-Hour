using UnityEngine;
using System.Collections;

public class JumpUp : PowerUpModifier {

    public override float getJumpHeightModifier()
    {
        return 10.0f;
    }

}
