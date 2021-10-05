using System.Collections;
using Player;
using UnityEngine;

public class ShieldActive : MonoBehaviour
{
    public static bool shieldActive;

    private void Start()
    {
        shieldActive = false;
    }

    private void Update()
    {
        Debug.Log("Shield Non Active");
        
        //condition when shield active
        if (shieldActive)
        {
            Debug.Log("Shield Active");
        }
        
        if (Input.GetKey(KeyCode.W) && DataItems.shield > 0)
        {
            StartCoroutine(nameof(Shield));
            DataItems.shield -= 1;
        }
    }

    IEnumerator Shield()
    {
        shieldActive = true;
        yield return new WaitForSeconds(5);
        shieldActive = false;
    }
}