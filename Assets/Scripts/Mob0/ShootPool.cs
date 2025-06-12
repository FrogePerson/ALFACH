using UnityEngine;
using Mirror;


public class ShootPool : NetworkBehaviour
{

    [SerializeField] private Transform _Magazin;
    [SerializeField] private int _sumOfObjects;
    [SerializeField] GameObject[] poolObject;
    SyncList<int> _SyncStat = new SyncList<int>();
    private int _stat=0;

    [Server]
    void Start()
    {
        for (int i=0;i<_sumOfObjects;i++){
                _SyncStat.Add(_stat);
            }
    }

    [Server]
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

    [Server]
    public void PoolGive(int i)
    {
        _SyncStat[i] = 0;
    }

    void Update()
    {
        for (int i=0;i<_sumOfObjects;i++){
                if(_SyncStat[i]==0)
                {
                    Bullet1Shoot BulletObj= poolObject[i].GetComponent<Bullet1Shoot>();
                    poolObject[i].transform.SetParent(_Magazin);
                    BulletObj._P1.SetActive(false);
                    BulletObj._P2.SetActive(false);
                    BulletObj._T.SetActive(false);
                    BulletObj._L.SetActive(false);
                    poolObject[i].GetComponent<Rigidbody>().velocity = new Vector3(0.0f,0.0f,0.0f);
                    poolObject[i].transform.position=transform.position;
                    poolObject[i].transform.eulerAngles = transform.eulerAngles;
                    poolObject[i].SetActive(false);
                }

                if(_SyncStat[i]==1)
                {
                    poolObject[i].SetActive(true);
                    poolObject[i].transform.SetParent(null);
                }
            }
    }
}
