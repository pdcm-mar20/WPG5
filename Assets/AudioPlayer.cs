using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class AudioPlayer : NetworkedBehaviour
{
    public AudioSource source;
    
    private void Awake()
    {
        if (IsLocalPlayer)
        {
            Debug.Log("Local");
            source.enabled = false;
            source.Stop();
        }
    }
}
