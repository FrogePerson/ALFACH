using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AttackJump : NetworkBehaviour
{
    [SerializeField] [SyncVar] float _NewAttackTime;

    private Move _Move;

    void Start()
    {
        _Move = GetComponent<Move>();
        Sett_NewAttackTime(0);
    }

    [Server]
    private void Sett_NewAttackTime(float i)
    {
        _NewAttackTime = Random.Range(i,15) + Time.time;
    }

    void Update()
    {
        if(((Time.time - _NewAttackTime) >0) && ((_Move._MobLive == 1) || (_Move._MobLive == 4)) && (_Move._IsJump == 0))
        {
            Attack();
        }
    }

    void OnTriggerStay(Collider other)
    {
        GameObject bridge = other.gameObject;
        if((bridge.tag == "Bridge") && ((_Move._MobLive == 1) || (_Move._MobLive == 4)) && (_Move._IsJump == 0))
        {
            Attack();
        }
    }

    [Server]
    private void Attack()
    {
        _Move._IsJump =1;
        Sett_NewAttackTime(5);
    }

}
