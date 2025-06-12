using UnityEngine;
using Mirror;

public class Pool : NetworkBehaviour
{
    [SerializeField] private int _sumOfObjects;
    [SerializeField] GameObject[] poolObject;
    SyncList<int> _SyncStat = new SyncList<int>();
    private int _stat=0;
    SyncList<int> _SyncGameObjectLife = new SyncList<int>();
    [SerializeField] private int _GameObjectLife=50;

    void Start()
    {
        for (int i=0;i<_sumOfObjects;i++){
                _SyncStat.Add(_stat);
                _SyncGameObjectLife.Add(_GameObjectLife);
            }
    }

    [Command]
    public void PoolGet()
    {
        for (int i=0;i<_sumOfObjects;i++){
                if(_SyncStat[i]==0)
                {
                    _SyncStat[i]=1;
                    break;
                }
            }
    }

    [Command]
    public void PoolGive(int i)
    {
        _SyncStat[i] = 0;
        _SyncGameObjectLife[i] =50;
    }

    [Command]
    public void PoolManager(int i)
    {
        if(_SyncStat[i] ==1)
        {
            _SyncGameObjectLife[i] -= 1;
        }
        
    }

    void Update()
    {
        for (int i=0;i<_sumOfObjects;i++){
                if(isLocalPlayer)
                {
                    PoolManager(i);
                }
                if(_SyncGameObjectLife[i] <0)
                {
                    PoolGive(i);
                }
                if(_SyncStat[i]==0)
                {
                    poolObject[i].SetActive(false);
                }

                if(_SyncStat[i]==1)
                {
                    poolObject[i].SetActive(true);
                }
            }
    }
}
