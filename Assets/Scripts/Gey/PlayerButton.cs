using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerButton : NetworkBehaviour
{
    [SyncVar]
    public int MobLive = 1;

    [Command]
    void PressButton()
    {
        MobLive = 2;
    }

    [Command]
    void UnpressButton()
    {
        MobLive = 1;
    }

    void OnTriggerStay(Collider collision)
    {
        //Debug.Log("COLL");
        if((collision.gameObject.tag == "Button") && (isLocalPlayer))
        {
            //button = collision.gameObject.GetComponent<Spawner>();
            //button.SpawnObjects();
            if (Input.GetKeyDown(KeyCode.F))
            {
               PressButton();
            }
            else
            {
                UnpressButton();
            }
        }
    }
}
