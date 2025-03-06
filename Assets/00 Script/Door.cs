using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         Observer.Notify("Complete", null);
      }
      
   }
}
