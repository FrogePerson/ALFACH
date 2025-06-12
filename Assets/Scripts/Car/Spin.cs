using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{

    public static float rotationSpeed = 6f;

    [SerializeField] private GameObject Wheel0;
    [SerializeField] private GameObject Wheel1;

    Rigidbody rb;

    Rigidbody rbWheel0;
    Rigidbody rbWheel1;
    Rigidbody rbWheel2;
    Rigidbody rbWheel3;
    float counter = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rbWheel0 = Wheel0.GetComponent<Rigidbody>();
        rbWheel1 = Wheel0.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rotationSpeed+=counter;
        counter+=0.005f;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, -rotationSpeed, 0));
        Vector3 force = transform.forward * (counter+50f);

        if(rotationSpeed >= 50)
        {
            rotationSpeed = 50;
        }
        rbWheel0.AddForce(force, ForceMode.Force);
        rbWheel1.AddForce(force, ForceMode.Force);
    }
}
