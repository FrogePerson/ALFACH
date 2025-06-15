using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    float _speed=0;

    public float newSpeed = 0;

    float rotation = 0; 

    public float speed 
    {   
        get
            {
                return _speed;
            }
        set 
            {
                spin1.GetComponent<Spin>().speed = value;
                spin2.GetComponent<Spin>().speed = value;
                _speed = value;
            }
    }

    public GameObject spin1;
    public GameObject spin2;

    void Start()
    {
        transform.rotation = spin1.transform.rotation;
    }

    void FixedUpdate()
    {
        if(newSpeed != speed)
        {
            speed = newSpeed;
        }

        spin1.transform.rotation = transform.rotation;

    }
}
