using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBall : MonoBehaviour {

    public int chargeAmount;

	// Use this for initialization
	void Start () {
        chargeAmount = 0;
	}
	
	void giveCharge(int amount)
    {
        chargeAmount += amount;
    }
}
