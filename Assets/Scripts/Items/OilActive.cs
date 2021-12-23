using System.Collections;
using MLAPI;
using UnityEngine;

public class OilActive : NetworkedBehaviour
{
    private void Update()
    {
        if (DataItems.oil && !DataItems.shield && IsLocalPlayer)
        {
            StartCoroutine(nameof(OilOn));
        }
    }
    
    public IEnumerator OilOn()
    {
        PlayerMovement.speed.Value = 3;
        yield return new WaitForSeconds(5);
        DataItems.oil = false;
        PlayerMovement.speed.Value = 5;
    }
}