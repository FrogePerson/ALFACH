using UnityEngine;
using Mirror;

public class B : NetworkBehaviour
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _pool;
    [SerializeField] private float _shootForce;
    [SerializeField] private int _i;

    [SyncVar]
    public int _bulletTime;


    private Rigidbody rb;

    [SyncVar]
    public float _moveWectorX= 0;
    [SyncVar]
    public float _moveWectorY= 0;
    [SyncVar]
    public float _moveWectorZ= 0;

    private Vector3 _shootWector = new Vector3(0.5f,0.5f,0);

    private Weapon _Weapon;
    //[Command]
    private void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        _Weapon = _weapon.GetComponent<Weapon>();
        GetVectorFromWeapon();
        ShootOnClient();
        SBulletBecup();
    }
    private void ShootOnClient()
    {
        _shootWector = new Vector3(_moveWectorX,_moveWectorY,_moveWectorZ);
        rb.AddForce(_shootWector.normalized*_shootForce,ForceMode.Impulse);
    }

    private void GetVectorFromWeapon()
    {
        _moveWectorX = _Weapon._shootWectorX;
        _moveWectorY = _Weapon._shootWectorY;
        _moveWectorZ = _Weapon._shootWectorZ;
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //rb.isKinematic = false;
        //rb.isKinematic = true;
        rb.velocity = new Vector3(0.0f,0.0f,0.0f);
        gameObject.transform.position = _pool.transform.position;
        ServerPoollGive();
    }

    //[Server]
    private void ServerPoollGive()
    {
        _weapon.GetComponent<PoollOfBullets>().PoolGive(_i);
    }
    private void FixedUpdate()
    {
        SBulletManager();
        if(_bulletTime <0)
        {
            rb.velocity = new Vector3(0.0f,0.0f,0.0f);
            gameObject.transform.position = _pool.transform.position;
            ServerPoollGive();
        }
    }
    [Server]
    private void SBulletManager()
    {
        _bulletTime -=1;
    }

    [Server]
    private void SBulletBecup()
    {
        _bulletTime =150;
    }

}
