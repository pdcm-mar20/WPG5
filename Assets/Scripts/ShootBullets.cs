using Mirror;
using UnityEngine;

public class ShootBullets : NetworkBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed;

    [SerializeField] private Transform bulletPos;
    void Update () 
    {
        if(isLocalPlayer && Input.GetKeyDown(KeyCode.P)) 
        {
            CmdShoot();
        }
    }
    
    [Command]
    void CmdShoot ()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 1.0f);
    }
}
