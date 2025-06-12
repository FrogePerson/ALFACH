using UnityEngine;
using Mirror;

public class TurretShot : NetworkBehaviour
{
    [SerializeField] private GameObject _shotPlace;
    [SerializeField] private GameObject _paticalSistem;

    [SyncVar] public int _MobLive=1;

    private void ChangeActive()
    {
        if(_MobLive == 0)
        {
            _paticalSistem.SetActive(false);
        }
        if(_MobLive==1)
        {
            _paticalSistem.SetActive(true);
        }
    }

    void Update()
    {
        ChangeActive();
        SerChangeMobLive(_MobLive);
    }

    [Server]
    private void SerChangeMobLive(int i)
    {
        _MobLive = i;
    }
}
