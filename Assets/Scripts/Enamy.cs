using UnityEngine;
using Mirror;

public class Enamy : NetworkBehaviour{
    [SyncVar]
    public int health = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            int damage = collision.gameObject.GetComponent<Damage>().damage;
            Damage(damage);
            if(health <=0)
            {
                DestroyObject();
                Destroy(gameObject);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    [Server]
    private void DestroyObject()
    {
        NetworkServer.Destroy(gameObject);
    }

    [Server]
    private void Damage(int damage)
    {
        health-=damage;
    }
}
