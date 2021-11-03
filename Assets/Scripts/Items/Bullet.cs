using System;
using MLAPI;
using MLAPI.NetworkedVar;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private NetworkedVarFloat speed = new NetworkedVarFloat(10);

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed.Value * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            var active = other.gameObject.GetComponent<ObstacleActive>();
            StartCoroutine(nameof(active.ObstacleOn));
        }
    }
}