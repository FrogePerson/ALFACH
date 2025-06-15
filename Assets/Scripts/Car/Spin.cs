using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    public float speed = 0f;

    float oldSpeed = 0f;

    [SerializeField] private GameObject Wheel0;
    [SerializeField] private GameObject Wheel1;

    Rigidbody rb;

    Rigidbody rbWheel0;
    Rigidbody rbWheel1;
    Rigidbody rbWheel2;
    Rigidbody rbWheel3;

    public static float rotationSpeed = 6f;
    public static float counter = 0f;
    public static float direction = 0f;

    public float boost = 40f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rbWheel0 = Wheel0.GetComponent<Rigidbody>();
        rbWheel1 = Wheel0.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        if (oldSpeed != speed)
        {
            rotationSpeed = 6f;
            counter = 0f;
            direction = speed;
            oldSpeed = speed;
        }

        if(direction != 0)
        {
            rotationSpeed+=counter;
            counter+=0.005f;
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -rotationSpeed* direction, 0));
            Vector3 force = transform.forward * (counter+boost) * direction;

            if(rotationSpeed >= 50)
            {
                rotationSpeed = 50;
            }
            rbWheel0.AddForce(force, ForceMode.Force);
            rbWheel1.AddForce(force, ForceMode.Force);
        }
        
    }
}
