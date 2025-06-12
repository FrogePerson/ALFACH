using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Attack : NetworkBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _target;
    [SerializeField] GameObject _Face;
    [SyncVar] [SerializeField] private int MobLive = 1;

    private Move _Move;

    private PlTarget _PlTarget;

    void Start()
    {
        _PlTarget = _target.GetComponent<PlTarget>();
        _Move = GetComponent<Move>();
    }

    [Server]
    void Update()
    {
        MobLive = 1;

        _player = _PlTarget._player;

        Ray ray = new Ray(_Face.transform.position, (_target.transform.position - _Face.transform.position));
        Debug.DrawRay(_Face.transform.position, (_target.transform.position - _Face.transform.position),Color.yellow);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){
            if((hit.collider.gameObject.tag != "Player") && (hit.collider.gameObject.tag != "Mob") && (hit.collider.gameObject.tag != "NotStatic"))
            {
                MobLive = 0;
            }
        }
        if((_Move._MobLive == 1) || (_Move._MobLive== 4))
        {
            if(MobLive ==1) 
            {
                _Move._MobLive= 4;
            }
            else
            {
                _Move._MobLive= 1;
            }
        }
        
    }
}
