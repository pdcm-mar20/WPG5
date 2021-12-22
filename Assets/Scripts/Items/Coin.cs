using System;
using System.Collections;
using MLAPI;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource source;

    private void Start()
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
                source.PlayOneShot(source.clip);
                DataItems.coin += 1;
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