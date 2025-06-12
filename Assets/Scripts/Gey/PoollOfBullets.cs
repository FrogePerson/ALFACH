using UnityEngine;
using Mirror;

public class PoollOfBullets : NetworkBehaviour
{
    [SerializeField] private GameObject _pool;
    [SerializeField] private int _sumOfObjects=4;
    [SerializeField] GameObject[] poolObject;
    SyncList<int> _SyncStat = new SyncList<int>();
    private int _stat=0;

    [Command]
    void Start()
    {
        for (int i=0;i<_sumOfObjects;i++){
                _SyncStat.Add(_stat);
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
    }

    void Update()
    {
        for (int i=0;i<_sumOfObjects;i++){
                if(_SyncStat[i]==0)
                {
                    poolObject[i].SetActive(false);
                    poolObject[i].transform.SetParent(_pool.transform);
                }

                if(_SyncStat[i]==1)
                {
                    poolObject[i].SetActive(true);
                    poolObject[i].transform.SetParent(null);
                }
            }
    }
}
