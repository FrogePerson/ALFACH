using UnityEngine;
using Mirror;

public class ParcticalLive : NetworkBehaviour
{
    [SerializeField] private float _bulletLive = 50;
    //public GameObject _spawnBullet;
    
    void FixedUpdate()
    {
        
        _bulletLive -=1;
        if (_bulletLive <= 0)
        {
            DestroyBullet();
            //print("Bullet died");
            Destroy(gameObject);
        }
    }

    [Command]
    private void DestroyBullet()
    {
        NetworkServer.Destroy(gameObject);

    }
}
