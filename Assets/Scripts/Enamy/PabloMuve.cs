using UnityEngine;
using Mirror;

public class PabloMuve : NetworkBehaviour
{
    //[SyncVar]
    [SerializeField] private float _pointX= 0f;
    //[SyncVar]
    [SerializeField] private float _pointY= 0f;
    //[SyncVar]
    [SerializeField] private float _pointZ= 0f;

    [SerializeField] private float _shootForce;

    private Rigidbody rb;
    private Vector3 _moveWector;
    private float flag = 1f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveWector = new Vector3(_pointX,_pointY,_pointZ);
        rb.AddForce(_moveWector.normalized*_shootForce,ForceMode.Impulse);
        _pointY+=0.001f;
        _pointY *= flag;
        if(_pointY >50f)
        {
            flag = -1f;
        }
        if(_pointY <0f)
        {
            flag = 1f;
        }
    }


}
