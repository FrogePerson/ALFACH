using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BodyRot : NetworkBehaviour
{
    [SerializeField] GameObject _Body;
    [SerializeField] GameObject _IdeRot;
    [SerializeField] int _Sas=0;
    [SerializeField] float _bodyRotSpeed;

    

    Quaternion _NewRot;

    private Move _Move;

    void Start()
    {
        _Move = GetComponent<Move>();
    }

    void Update()
    {
        bodyRot();
    }

    private void bodyRot()
    {
        if((_Move._MobLive == 1) || (_Move._MobLive == 4))
        {
            RaycastHit hit;
            if(Physics.Raycast(_Body.transform.position, Vector3.down, out hit, 3)) 
            {
                if(hit.collider.gameObject.tag == "Plane")
                {
                    Quaternion targetRot = Quaternion.FromToRotation(Vector3.up, hit.normal) * _IdeRot.transform.rotation;
                    _Body.transform.rotation = Quaternion.Lerp(_Body.transform.rotation, targetRot, _bodyRotSpeed* Time.deltaTime);
                    
                }
                else 
                {
                    _Body.transform.rotation = _IdeRot.transform.rotation;
                    
                }
                
            }   
        }
    }
}
