using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class Move : NetworkBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed = 0f;
    //[SerializeField] private float _targetDistanse = 0.1f;

    [SerializeField] private float _newTargetTime;

    [SyncVar]
    public int _MobLive= 1;
    [SyncVar]
    public int _hp= 5;
    [SyncVar] public int _IsJump =0;
    [SerializeField] [SyncVar] int _IsGouded=0;
    //[SerializeField] private Vector3 _moveTarget;

    private UnityEngine.AI.NavMeshAgent _agent;
    private Animator _anim;
    private Rigidbody _rb;
    private CapsuleCollider _Collider;
    private Jump _Jump;
    private float _hitTimer = 0;
    [SerializeField] private int _lastMobLive = 0;

    [SerializeField] private GameObject _GameObjectPhis;
    [SerializeField] private GameObject _GameObject;
    [SerializeField] private float _bodyRotSpeed=0.01f;

    void Start()
    {
        //NewTarget();
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _anim =GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _Collider = GetComponent<CapsuleCollider>();
        _Jump = GetComponent<Jump>();
    }

    void CheckGround()
    {
        if((Physics.Raycast(transform.position, Vector3.down, 2.5f)))
        {
            Fix_IsGrouded(1);
            
        }
        else
        {
            Fix_IsGrouded(0);//in air
        }
    }

    [Server]
    private void Fix_IsJump(int i)
    {
        _IsJump = i;
    }

    [Server]
    private void Fix_IsGrouded(int i)
    {
        _IsGouded = i;
    }

    void FixedUpdate()
    {
        //_moveTarget = new Vector3(_moveWectorX,transform.position.y,_moveWectorZ);
        
        //var direction = (_moveTarget).normalized;
        //transform.Translate(direction* _speed);
        
        //if((Time.time - _newTargetTime) > 5)
        //{
            //NewTarget();
        //}
        CheckGround();
        if(_IsGouded == 0)
        {
            _Jump.Gravity();
        }
        if(_IsJump==1)
        {
            _Jump.jump();
            Fix_IsJump(2);
        }
        else if((_IsJump==2) && (_IsGouded==0))
        {
            Fix_IsJump(3);
        }
        else if(_IsJump == 3)
        {
            if(_IsGouded==1)
            {
                Fix_IsJump(0);
            }
            else
            {
                
            }
            
        }
        if(_MobLive == 1)//Walk
        {
            if(_IsJump == 0)
            {
                _agent.SetDestination(_target.transform.position);
                _agent.enabled = true;
                _anim.SetInteger("MobLive", 1);
            }
            else
            {
                _agent.enabled = false;
                _anim.SetInteger("MobLive", 1);
            }
            
            
            //transform.Translate(new Vector3(0,0,transform.forward.z).normalized*_speed);
        }

        else if(_MobLive == 2) //Stop
        {
            _anim.SetInteger("MobLive", 2);
            _agent.enabled = false;
        }

        else if(_MobLive == 3) //Hit
        {
            
            if((Time.time - _hitTimer)>3)
            {
                 MobLiveLastMobLive();
                _rb.isKinematic = true;
                _rb.isKinematic = false;
            }

        }

        else if(_MobLive == 4) //Attack
        {
            if(_IsJump ==0)
            {
                _agent.enabled = true;
                _anim.SetInteger("MobLive", 1);
                _agent.SetDestination(_target.transform.position);
                
            }
            else
            {
                _agent.enabled = false;
                _anim.SetInteger("MobLive", 1);
            }
            //transform.LookAt(_target.transform);
            Rotate();
            
        }

        else if(_MobLive == 0) //Die
        {
            _anim.SetInteger("MobLive", 0);
            _GameObjectPhis.SetActive(true);
            _GameObject.SetActive(false);
            Destroy(_rb);
            Destroy(_Collider);
            Destroy(_agent);
            Destroy(_target);
            if((Time.time - _hitTimer) > 10)
            {
                Destroy(gameObject);
                NetworkServer.Destroy(gameObject);
            }
        }

        
        //_moveTarget = new Vector3(0,0,1);
        //var direction = (_moveTarget).normalized;
        //transform.Translate(direction* _speed);
        //transform.LookAt(_target);

    }

    [Server]
    private void NewTarget()
    {
        //_moveWectorX = Random.Range(-10,10);
        //_moveWectorZ = Random.Range(-10,10);
        _newTargetTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            _agent.enabled = false;
            _hitTimer = Time.time;
            _hp -= collision.gameObject.GetComponent<Damage>().damage;
            if(_hp <=0)
            {
                MobLive0();
            }
            else
            {
                if(_MobLive != 3)
                {
                    _lastMobLive = _MobLive;
                }
                MobLive3();
                _anim.SetInteger("MobLive", 3);
            }
        }
    }
    [Server]
    private void MobLive0()
    {
        _MobLive = 0;
    }
    [Server]
    private void MobLive3()
    {
        _MobLive = 3;
    }
    [Server]
    private void MobLiveLastMobLive()
    {
        _MobLive = _lastMobLive;
    }

    private void Rotate()
    {
        Vector3 directionToTarget = transform.position - _target.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget*-1);
        //Quaternion.Inverse(targetRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _bodyRotSpeed);
    }
}
