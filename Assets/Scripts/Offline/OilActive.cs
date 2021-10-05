using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class OilActive : MonoBehaviour
{
    public static bool oilActive;

    private void Start()
    {
        oilActive = false;
    }

    private void Update()
    {

        if (DataItems.oil)
        {
            StartCoroutine(nameof(SpeedSlowActive));
            DataItems.oil = false;
            Debug.Log("Oil Active");
        }
        else
        {
            Debug.Log("Oil Non Active");
        }
    }

    IEnumerator SpeedSlowActive()
    {
       
        Offline.PlayerMovement.speed = 0;
        yield return new WaitForSeconds(3);
        Offline.PlayerMovement.speed = 5;
    }
}