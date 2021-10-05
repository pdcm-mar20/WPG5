using System;
using Player;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         DataItems.waterBall += 1;
         Destroy(gameObject);
      }
   }
}