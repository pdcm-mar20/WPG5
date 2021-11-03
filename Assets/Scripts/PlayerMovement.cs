using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using UnityEngine;

public class PlayerMovement : NetworkedBehaviour
{
    public static NetworkedVarFloat speed = new NetworkedVarFloat(5);
    public NetworkedVarFloat jump = new NetworkedVarFloat(10);
    [SerializeField] private new GameObject camera;

    private void Start()
    {
        DataItems.waterBall = 3;
        if (!IsLocalPlayer)
        {
            camera.SetActive(false);
        }
    }

    private void Update()
    {
        if (IsLocalPlayer)
        {
            Move(InputPlayer());
            Jump();
        }
    }

    [ServerRPC]
    private static Vector2 InputPlayer()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var dir = new Vector3(h, 0, v);
        return dir;
    }

    [ServerRPC]
    private void Move(Vector3 dir)
    {
        transform.position += Vector3.right * (Time.deltaTime * speed.Value);
    }

    [ServerRPC]
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * (jump.Value * Time.deltaTime);
        }
    }
}