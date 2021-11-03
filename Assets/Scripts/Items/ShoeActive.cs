using System.Collections;
using UnityEngine;

public class ShoeActive : MonoBehaviour
{
    void Update()
    {
        if (DataItems.shoe)
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
