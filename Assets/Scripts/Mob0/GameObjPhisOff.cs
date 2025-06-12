using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjPhisOff : MonoBehaviour
{
     [SerializeField] private GameObject _GameObjectPhis;

   private void OnEnable()
   {
        _GameObjectPhis.SetActive(false);
   }
}
