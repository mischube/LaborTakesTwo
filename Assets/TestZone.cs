using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;

public class TestZone : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      var manager = GameManager.Instance;
      
      if (other.CompareTag("Player"))
      {
         Debug.Log("Player entered platform");
         manager.SwitchScene();
      }
   }
}
