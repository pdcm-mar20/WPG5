﻿using System.Collections;
using MLAPI;
using UnityEngine;

public class ShieldActive : NetworkedBehaviour
{
    private float timer = 5;
    public GameObject shieldActive;
    private void Update()
    {
        if (IsLocalPlayer)
        {
            if (DataItems.shield)
            {
                shieldActive.SetActive(true);
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    DataItems.shield = false;
                    timer = 5;
                }
            }
            else
            {
                shieldActive.SetActive(false);
            }
        }
    }
    /*
    IEnumerator ShieldOn()
    {
        yield return new WaitForSeconds(8);
        //DataItems.shield = false;
        //Debug.Log("Shield Mati");
    }
    */
}