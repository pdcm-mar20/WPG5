using System.Collections;
using UnityEngine;

public class OilActive : MonoBehaviour
{
    private void Update()
    {
        if (DataItems.oil && !DataItems.shield)
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