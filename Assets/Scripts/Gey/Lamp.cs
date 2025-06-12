using UnityEngine;
using Mirror;

public class Lamp : NetworkBehaviour
{
    [SerializeField] private GameObject _lamp;
    [SyncVar]
    public int LampLife = 0;

    void Start()
    {
        //_lamp.SetActive(false);
    }

    [Command]
    public void LampOn()
    {
        LampLife =1;
        //_lamp.SetActive(true);
    }

    [Command]
    public void LampOff()
    {
         LampLife =0;
        //_lamp.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isLocalPlayer)
            {
                if (LampLife == 1)
                {
                    LampOff();
                }
                else
                {
                    LampOn();
                }
                
            }
            
            //print("Bullet spawned");
        }

        if(LampLife==0)
        {
            _lamp.SetActive(false);
        }
        if(LampLife==1)
        {
            _lamp.SetActive(true);
        }
    }
}
