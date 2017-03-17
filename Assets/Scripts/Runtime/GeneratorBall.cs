using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GeneratorBall : NetworkBehaviour {

    [SyncVar]
    public int chargeAmount;
    
	public void giveCharge(int amount)
    {
        CmdIncreaseChargeAmount(amount);
    }

    [Command]
    void CmdIncreaseChargeAmount( int amount)
    {
        chargeAmount += amount;
    }
}
