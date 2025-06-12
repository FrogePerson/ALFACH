using UnityEngine;
using Mirror;
public class Bullet1Shoot : NetworkBehaviour
{
    [SerializeField] private GameObject _ShootEffect;

    [SerializeField] public GameObject _P1;
    [SerializeField] public GameObject _P2;
    [SerializeField] public GameObject _T;
    [SerializeField] public GameObject _L;

    [SerializeField] private GameObject _ShootPlace; 
    [SerializeField] private Vector3 _shootWector;
    [SerializeField] private float _shootForce=2;
    [SerializeField] private int _i;
    [SerializeField] private float _deadTime;
    [SerializeField] [SyncVar] private float _ShootTime;
    private ShootPool _pool;
    private Rigidbody _rb;
    [SyncVar] [SerializeField] private float _TimeMinusShoottime;
    void OnEnable()
    {
        _rb = GetComponent<Rigidbody>();
        _pool = _ShootPlace.GetComponent<ShootPool>();
        _shootWector = transform.forward *-1;
        _rb.AddForce(_shootWector.normalized*_shootForce,ForceMode.Impulse);
        //transform.SetParent(null);
        _P1.SetActive(true);
        _P2.SetActive(true);
        _T.SetActive(true);
        _L.SetActive(true);
        _ShootEffect.SetActive(true);
        SetTime();
        
        
    }

    [Server]
    private void SetTime()
    {
        _ShootTime = Time.time;
        _TimeMinusShoottime = 0;
    }

    

    [Server]
    private void TimeminusShootTime()
    {
        _TimeMinusShoottime =Time.time - _ShootTime;
    }
    
    void Update()
    {
        TimeminusShootTime();
        if((_TimeMinusShoottime > _deadTime) )
        {
           _pool.PoolGive(_i);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _pool.PoolGive(_i);
    }
}
