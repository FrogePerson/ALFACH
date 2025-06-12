using UnityEngine;
using Mirror;
[RequireComponent(typeof(PlaeM))]
public class PlaeR : NetworkBehaviour
{   
    
    [SerializeField]
    private float speed = 15f;
    private float lspeed = 1.0f;
    
    private PlaeM mot;

    void Start(){
        mot = GetComponent <PlaeM> ();
    }
    void Update(){
        //if (!isLocalPlayer) return;
        float xMove = Input.GetAxisRaw ("Horizontal");
        float yMove = Input.GetAxisRaw ("Vertical");

        Vector3 moveH = transform.right * xMove;
        Vector3 moveV = transform.forward * yMove;

        Vector3 vell = (moveH + moveV).normalized * speed;

        mot.Move(vell);

        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rott = new Vector3(0f,yRot,0f)*lspeed;

        mot.Rotate(rott);
        float zRot = Input.GetAxisRaw("Mouse Y");
        //Debug.Log(zRot);
        Vector3 rottz = new Vector3(zRot,0f,0f)*lspeed;

        mot.Rotatez(rottz);


    }
}