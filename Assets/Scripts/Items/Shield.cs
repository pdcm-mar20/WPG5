using MLAPI;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<NetworkedObject>();
            if (player.IsLocalPlayer)
                DataItems.shield = true;
            Destroy(gameObject);
        }
    }
}