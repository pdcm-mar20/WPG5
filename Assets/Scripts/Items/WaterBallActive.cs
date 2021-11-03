using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class WaterBallActive : NetworkedBehaviour
{
    [SerializeField] private GameObject waterBall;
    [SerializeField] private Transform pos;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && DataItems.waterBall > 0 && IsLocalPlayer)
        {
            DataItems.waterBall -= 1;
            CreateBullet();
        }
    }
    
    void CreateBullet()
    {
        Instantiate(waterBall, pos.position, Quaternion.identity);
    }
}
