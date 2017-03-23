using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BearHitMe : NetworkBehaviour {



	void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.transform.parent.gameObject.name);
        if(other.gameObject.transform.parent.gameObject.name.Equals("GroundPlatformer(Clone)"))
        {
            CmdDestroyMe();
        }
    }

    [Command]
    void CmdDestroyMe()
    {
        RpcDestroyMe();
    }

    [ClientRpc]
    void RpcDestroyMe()
    {
        Destroy(gameObject);
    }
}
