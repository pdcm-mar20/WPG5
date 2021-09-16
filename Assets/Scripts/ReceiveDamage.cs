using Mirror;
using UnityEngine;

public class ReceiveDamage : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 10;
    [SyncVar]
    private int currentHealth;
    [SerializeField]
    private string tag;
    [SerializeField]
    private bool destroyOnDeath;
    private Vector2 initialPosition;
    // Use this for initialization
    void Start () 
    {
        this.currentHealth = this.maxHealth;
        this.initialPosition = this.transform.position;
    }
    void OnTriggerEnter2D (Collider2D collider) 
    {
        if(collider.CompareTag("Bullet") && !isLocalPlayer) 
        {
            this.TakeDamage(1);
            Destroy(collider.gameObject);
        }
    }
    void TakeDamage (int amount) 
    {
        if(this.isServer) 
        {
            this.currentHealth -= amount;
            if(this.currentHealth <= 0) 
            {
                if(this.destroyOnDeath) 
                {
                    Destroy(this.gameObject);
                } 
                else 
                {
                    this.currentHealth = this.maxHealth;
                    RpcRespawn();
                }
            }
        }
    }
    [ClientRpc]
    void RpcRespawn () 
    {
        this.transform.position = this.initialPosition;
    }
}
