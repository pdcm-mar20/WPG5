using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private void Start()
    {
        Debug.Log("is Local " +isLocalPlayer);
        Debug.Log("is Client" + isClient +" "+isClientOnly);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isLocalPlayer)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}
