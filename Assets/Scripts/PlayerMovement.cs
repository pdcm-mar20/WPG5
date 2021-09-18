using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]private float speed = 5;

    [SerializeField] private new GameObject camera;
    private void Start()
    {
        if (!isLocalPlayer)
        {
            camera.SetActive(false);
        }
        
        Debug.Log("is Local 1 " +isLocalPlayer);
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        
        Move(InputPlayer());
        Jump();
    }

    private static Vector2 InputPlayer()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        
        var dir = new Vector3(h, 0, v);
        return dir;
    }

    private void Move(Vector3 dir)
    {
        transform.position += Vector3.right * (Time.deltaTime * speed);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * (5f * Time.deltaTime);
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            
        }
    }
}