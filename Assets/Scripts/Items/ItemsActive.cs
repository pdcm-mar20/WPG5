using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsActive : MonoBehaviour
{
    [SerializeField] private Text coin;

    // Update is called once per frame
    void Update()
    {
        coin.text = DataItems.coin.ToString();
    }
}
