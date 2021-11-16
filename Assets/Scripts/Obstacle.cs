using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!DataItems.shield)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<NetworkedObject>();
                if(player.IsLocalPlayer && !DataItems.shield)
                    DataItems.obstacle = true;
                Destroy(gameObject);
            }
        }
        
    }
}
