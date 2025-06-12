using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Jump : NetworkBehaviour
{
   [SerializeField] private float _JumpForceUp;
   [SerializeField] private float _JumpForceDown;
   [SerializeField] private float _JumpForceForward;
   
   private Rigidbody _rb;
   //private Move _move;

   void Start()
   {
    _rb = GetComponent<Rigidbody>();
   //_move= GetComponent<Move>();
   }

   public void Gravity()
   {
        Vector3 jumpWector = Vector3.down;
        _rb.AddForce(jumpWector.normalized*_JumpForceUp);
   }

   public void jump()
   {
        Vector3 jumpWector = transform.forward;
        _rb.AddForce(jumpWector.normalized*_JumpForceForward,ForceMode.Impulse);
        jumpWector = Vector3.up;
        _rb.AddForce(jumpWector*_JumpForceUp,ForceMode.Impulse);
   }
}
