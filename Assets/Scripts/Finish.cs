using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI.Messaging;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    private static List<string> listName = new List<string>();
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;

    private int count = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (listName.Count == 1)
        {
            Debug.Log("Finishku "+listName.Count);
            SceneManager.LoadScene("States");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var name = other.gameObject.GetComponentInChildren<Text>();
            rigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
            spriteRenderer = other.gameObject.GetComponent<SpriteRenderer>();
            boxCollider2D = other.gameObject.GetComponent<BoxCollider2D>();
            Debug.Log(name.text);
            listName.Add(name.text);
            count++;
            ComponentActive(false);
            Debug.Log("Finish");
        }
    }

    public static List<string> GetListWinner()
    {
        return listName;
    }


    [ClientRPC]
    void ComponentActive(bool state)
    {
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        spriteRenderer.enabled = state;
        boxCollider2D.enabled = state;
    }
}