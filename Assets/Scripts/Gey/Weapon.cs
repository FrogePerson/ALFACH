using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Weapon :NetworkBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _particalSystem;
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _spawnBullet;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _spread;
    [SerializeField] private float _bulletLive = 50;

    

    [SyncVar]
    public float _shootWectorX= 0;
    [SyncVar]
    public float _shootWectorY= 0;
    [SyncVar]
    public float _shootWectorZ= 0;

    public Vector3 mainVector;

    private Pool _Pool;
    private PoollOfBullets _PoollOfBullets;

    void Start()
    {
        _Pool = GetComponent<Pool>();
        _PoollOfBullets = GetComponent<PoollOfBullets>();
    }

    [Command]
    private void Shoot()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(50);
        }

        Vector3 dirWithoutSpread=targetPoint-_spawnBullet.transform.position;

        float x = Random.Range(-_spread,_spread);
        float y = Random.Range(-_spread,_spread);

        Vector3 dirWithSpread = dirWithoutSpread + new Vector3(x,y,0);
        mainVector = dirWithoutSpread;

        _shootWectorX = mainVector.x;
        _shootWectorY = mainVector.y;
        _shootWectorZ = mainVector.z;

        //GameObject currentBullet= Instantiate(_bullet, _spawnBullet.transform.position, Quaternion.identity);
        //currentBullet.GetComponent<Rigidbody>().AddForce(dirWithoutSpread.normalized*_shootForce,ForceMode.Impulse);
        
        //print("Kall");
        //currentBullet.GetComponent<Bullet>().StartPos(_spawnBullet.position);
        //NetworkServer.Spawn(currentBullet);

        //GameObject particalSystem= Instantiate(_particalSystem, _spawnBullet.transform.position, transform.rotation);
        //particalSystem.GetComponent<ParcticalLive>()._spawnBullet=_spawnBullet;
        //particalSystem.transform.parent= _spawnBullet.transform;
        //NetworkServer.Spawn(particalSystem);

        //_particalSystem.SetActive(true);

        
        //currentBullet.GetComponent<NetworkIdentity>().Spawn();

        

        //GameObject currentBullet= Instantiate(_bullet, _spawnBullet.position, Quaternion.identity);
        //var currentBullet = (GameObject)Instantiate(_bullet, _spawnBullet.position, Quaternion.identity);
        //var instanceNetworkObject = currentBullet.GetComponent<NetworkObject>();
        //currentBullet.transform.position = _spawnBullet.position;
        //currentBullet.transform.forward = dirWithSpread.normalized;
        //currentBullet.GetComponent<Rigidbody>().AddForce(dirWithoutSpread.normalized*_shootForce,ForceMode.Impulse);
        //NetworkIdentity.Instantiate( currentBullet, _spawnBullet.position, Quaternion.identity);
        //currentBullet.GetComponent<Rigidbody>().transform.position=_spawnBullet.position;
        //NetworkServer.Spawn(currentBullet);
        
    }

    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isLocalPlayer)
            {
                
                Shoot();
                _Pool.PoolGet();
                _PoollOfBullets.PoolGet();
            }
            
            //print("Bullet spawned");
        }
    }
}
