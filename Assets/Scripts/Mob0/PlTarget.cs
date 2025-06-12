using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlTarget : NetworkBehaviour
{
    [SerializeField] private float _timer;
    private float mindist = 1000f;
    private float dist;
    [SerializeField] [SyncVar] private int _NumeroOfPlayer = 0;
    [SerializeField] private GameObject[] _players;
    [SerializeField] private Transform _Mob0;

    [SerializeField] public GameObject _player;
    
    private void OnEnable()
    {
        transform.SetParent(null);
        FindPlayer();
    }

    [Server]
    private void FindPlayer()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        _timer = Time.time;
        mindist = 1000f;
        for (int i=0;i<_players.Length;i++){
                dist = Vector3.Distance(_Mob0.position,_players[i].transform.position);
                if(mindist > dist)
                {
                    mindist = dist;
                    _NumeroOfPlayer = i;
                }
        }
        _player = _players[_NumeroOfPlayer];
    }

    void Update()
    {
        if(_players.Length ==0)
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
        }
        else
        {
            gameObject.transform.position = _players[_NumeroOfPlayer].transform.position;
        }
        if((Time.time - _timer)>3)
        {
            FindPlayer();
        }
    }
}
