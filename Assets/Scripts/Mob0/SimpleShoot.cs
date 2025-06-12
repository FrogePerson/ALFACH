using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SimpleShoot : NetworkBehaviour
{
    [SerializeField] private GameObject _ShootPlace;
    private ShootPool _shootPool;
    private Move _move;
    [SerializeField] private float _fireTime;
    [SyncVar] private float _shootTime;
    
    [Server]
    private void Shoot()
    {
        _shootPool.PoolGet();
        _shootTime = Time.time;
    }

    void Start()
    {
        _shootPool = _ShootPlace.GetComponent<ShootPool>();
        _move = GetComponent<Move>();
        _shootTime = Time.time;
    }

    void Update()
    {
        if((_move._MobLive == 4) && ((Time.time - _shootTime) > _fireTime))
        {
            Shoot();
            
        }
        
    }
}
