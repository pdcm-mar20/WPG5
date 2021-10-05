using System;
using Player;
using UnityEngine;

public class Oil : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      DataItems.oil = true;
      Destroy(gameObject);
    }
  }
}