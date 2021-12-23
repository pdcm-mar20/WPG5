using System.Collections;
using MLAPI;
using UnityEngine;

public class ShoeActive : NetworkedBehaviour
{
    void Update()
    {
        if (DataItems.shoe && IsLocalPlayer)
        {
            StartCoroutine(nameof(SpeedActive));
        }
    }

    IEnumerator SpeedActive()
    {
        PlayerMovement.speed.Value = 10;
        yield return new WaitForSeconds(5);
        PlayerMovement.speed.Value = 5;
        DataItems.shoe = false;
    }
}
