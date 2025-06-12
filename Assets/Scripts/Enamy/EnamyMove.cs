using UnityEngine;
using Mirror;

public class EnamyMove : NetworkBehaviour
{
    [SyncVar]
    public float _shootForce = 0;
    [SyncVar]
    public float _moveWectorX= 0;
    [SyncVar]
    public float _moveWectorY= 0;
    [SyncVar]
    public float _moveWectorZ= 0;
    
    private Vector3 _moveWector;

    //[Server]
    void Update()
    {
        _moveWector = new Vector3(_moveWectorX,_moveWectorY,_moveWectorZ);
        gameObject.GetComponent<Rigidbody>().AddForce(_moveWector.normalized*_shootForce,ForceMode.Impulse);
    }
}
