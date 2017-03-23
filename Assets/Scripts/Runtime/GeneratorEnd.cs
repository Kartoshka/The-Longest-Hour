using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GeneratorEnd : NetworkBehaviour {

    public Transform startPosForBall;
    public GameObject targetDoor;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("burh");
        if(other.gameObject.tag.Equals("generatorBall"))
        {
            GeneratorBall gb = other.gameObject.GetComponent<GeneratorBall>();
            if(gb.chargeAmount == 100)
            {
                CmdDestroyStuff();
            }
            else
            {
                gb.transform.position = startPosForBall.position;
            }
        }
    }

    [Command]
    void CmdDestroyStuff()
    {
        RpcDestroyStuff();
    }

    [ClientRpc]
    void RpcDestroyStuff()
    {
        Destroy(targetDoor);
    }
}
