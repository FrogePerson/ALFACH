using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Target : NetworkBehaviour
{
    [SerializeField] private Transform _Mob0;
    [SerializeField] private float _timer;
    private int _randNumero;
    private Vector3 _pos;

    
    private void OnEnable()
    {
        transform.SetParent(null);
    }

    [Server]
    private void OnEnableSer()
    {
        _timer = Time.time;
        _randNumero = Random.Range(5,15);
    }

    void Update()
    {
        if((Time.time - _timer) >5+_randNumero)
        {
            SetNewPoint();
            gameObject.transform.position=_pos;
        }
    }

    [Server]
    private void SetNewPoint()
    {
        _pos =new Vector3(Random.Range(_Mob0.position.x-60,_Mob0.position.x+60),Random.Range(_Mob0.position.y-5,_Mob0.position.y+5),Random.Range(_Mob0.position.z-60,_Mob0.position.z+60));
        _timer = Time.time;
        _randNumero = Random.Range(5,15);
    }


}
