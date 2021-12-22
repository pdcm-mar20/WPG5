using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class Shoe : MonoBehaviour
{
    
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<NetworkedObject>();
            if (player.IsLocalPlayer)
            {
                source.Play();
                DataItems.shoe = true;
            }

            StartCoroutine("DestroyItems");
        }
    }
    
    IEnumerator DestroyItems()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}