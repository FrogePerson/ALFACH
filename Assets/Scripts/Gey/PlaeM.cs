using UnityEngine;
using Mirror;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlaeM : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;
    private Rigidbody rb;
    [SerializeField]
    private Transform tr;
    [SerializeField]
    private Rigidbody trHand;
    private Vector3 vell=Vector3.zero;
    private Vector3 rote=Vector3.zero;
    private Vector3 rotez=Vector3.zero;
    private float yRot=0;
    private Vector3 Vek=Vector3.zero;
    float xRot=0.0f;
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _gravity=15f;
    [SerializeField] private float _jumpForce=8.0f;
    [SerializeField] private float _slopeForce=5.0f;
    [SerializeField] private float _slopeRay =1.5f;
    [SerializeField] private float _speedAir = 2;

    private CharacterController _controller;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _moveDirectionFixed = Vector3.zero;
    private bool isslope= false;
    private bool isTerrain=false;


    private void Awake(){
        _controller = GetComponent <CharacterController>();
    }

   
    
    void Start(){
        rb = GetComponent <Rigidbody> ();
        //trHand = GetComponent <Rigidbody> ();
        //cam = Camera.main;
        //tr = GetComponent <Transform> ();
        xRot=0.0f;

    }
    public void Move(Vector3 vel){
    vell = vel;
    
    }
    public void Rotate(Vector3 rot){
    rote = rot;
    
    }
    public void Rotatez(Vector3 rotz){
    rotez = rotz;
    
    }

    private void OnCollisionEnter(Collision collision)
    {
         if(collision.gameObject.tag == "Terrain")
         {
            isTerrain = true;
         }
         else
         {
            isTerrain = false;
         }
    }
    
    void Update(){
        //Debug.Log(-rotez);
        if((_controller.isGrounded) && (isslope==false))
        {
            SetMoveDirection();
            if(Input.GetButton("Jump"))
            {
                Jump();
            }
            _controller.Move(_moveDirection*Time.deltaTime);
        }
        else
        {
            if(isslope==false)
            {

                SetMoveAirDirection();
            }
            _controller.Move(_moveDirectionFixed*Time.deltaTime);
        }
        _moveDirection.y -= _gravity * Time.deltaTime;
       
        
       
    }

    private void SetMoveDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput,0.0f,verticalInput);
        inputDirection=transform.TransformDirection(inputDirection);
        _moveDirection = inputDirection*_speed;
        
    }
    private void SetMoveAirDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput,0.0f,verticalInput);
        inputDirection=transform.TransformDirection(inputDirection);
        _moveDirectionFixed=_moveDirection;
        _moveDirectionFixed.x += inputDirection.x*_speedAir;
        _moveDirectionFixed.z += inputDirection.z*_speedAir;
        
    }

    private void Jump()
    {
        _moveDirection.y=_jumpForce;
    }

    void FixedUpdate(){
        PerformRott();
        //Handwove();
        Slope();
        
        
    }

    private void Slope()
    {
        if((Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _slopeRay) == false))
        {
            isslope=false;
            return;
        }
        
        if((Vector3.Angle(hit.normal, Vector3.up)> _controller.slopeLimit) && (isTerrain == false))
        {
            _moveDirection.x += (1f - hit.normal.y)*hit.normal.x* _slopeForce;
            _moveDirection.z += (1f - hit.normal.y)*hit.normal.z* _slopeForce;
            _moveDirection.y -= _slopeForce;
            isslope=false;
            _controller.Move(_moveDirection*Time.deltaTime);
        }
    }

    void Handwove(){
        yRot=tr.rotation.y;
        Vek = new Vector3(0f,yRot,0f);
        trHand.transform.Rotate(Vek);
    }
    void PerformMove(){
    
    }
    void PerformRott(){
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(rote));
        //Debug.Log(trHand.transform.rotation.x);
        //if ((trHand.transform.rotation.x-rotez.x)<(-0.57f)){
            //rotez.x=0.0f;
        //}
        rb.transform.Rotate(rote);
        //cam.transform.Rotate(-rotez);
        //trHand.transform.Rotate(-rotez);
        //if (cam !=null){ 
        //}
        xRot+=rotez.x;
        xRot=Mathf.Clamp(xRot,-75,75);
        cam.transform.localEulerAngles= new Vector3(-xRot,0f,0f);
        trHand.transform.localEulerAngles = new Vector3(-xRot,0f,0f);
    }
    
    
}
