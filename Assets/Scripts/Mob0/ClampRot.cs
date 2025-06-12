using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampRot : MonoBehaviour
{
    [SerializeField] private float maxRotationX = 45f;
    [SerializeField] private float minRotationX = -45f;
    [SerializeField] private float rotx;

    void Update()
    {
        rotx = transform.eulerAngles.x;
    }
}
