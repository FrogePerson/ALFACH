using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Spawner : NetworkBehaviour
{
    //[SyncVar]
    //public int MobLive = 1;
    [SerializeField] private GameObject _SpawnedObject;
    [SerializeField] private Transform _SpawnObject;
    private NetworkIdentity networkitem;

    
    void Start()
    {
        //networkitem = GetComponent<NetworkIdentity>();
        //networkitem.AssignClientAuthority(connectionToClient);
        //Debug.Log("TrySpawn");
        //SpawnObjects();
    }
    [Server]
    public void SpawnObjects()
    {
        GameObject SpawnedObject= Instantiate(_SpawnedObject, _SpawnObject.position, Quaternion.identity);
        NetworkServer.Spawn(SpawnedObject);
    }
    [Server]
    void OnTriggerStay(Collider other)
        {
            GameObject player = other.gameObject;
            //SpawnObjects();
            if((player.tag == "Player"))
            {
                int PlayerButtonLive = player.GetComponent<PlayerButton>().MobLive;
                if(PlayerButtonLive == 2)
                {
                    SpawnObjects();
                }
            }
        }
    void Update()
    {
        
        //SpawnObjects();
        //SpawnObjects();
    }

    
}
