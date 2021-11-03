using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class Oil : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<NetworkedObject>();
            if (player.IsLocalPlayer)
                DataItems.oil = true;
            Destroy(gameObject);
        }
    }
}