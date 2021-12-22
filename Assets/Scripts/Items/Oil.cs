using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class Oil : MonoBehaviour
{
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!DataItems.shield)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var player = other.gameObject.GetComponent<NetworkedObject>();
                if (player.IsLocalPlayer)
                {
                    source.PlayOneShot(source.clip);
                    DataItems.oil = true;
                }

                StartCoroutine("DestroyItems");
            }
        }
    }
    
    IEnumerator DestroyItems()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}