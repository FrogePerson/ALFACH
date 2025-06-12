using UnityEngine;
using Mirror;

public class Plae : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] compsD;
    void Start (){
        if (!isLocalPlayer){
            for (int i=0;i<compsD.Length;i++){
                compsD[i].enabled = false;
            }
        }
    }
}
