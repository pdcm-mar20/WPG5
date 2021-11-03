using System.Collections;
using UnityEngine;

public class ShieldActive : MonoBehaviour
{
    private void Update()
    {
        if (DataItems.shield)
        {
            StartCoroutine(nameof(ShieldOn));
        }
    }

    IEnumerator ShieldOn()
    {
        yield return new WaitForSeconds(5);
        DataItems.shield = false;
    }
}