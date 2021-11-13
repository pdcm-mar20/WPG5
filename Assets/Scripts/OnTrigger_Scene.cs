using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnTrigger_Scene : MonoBehaviour
{
    public int index;
    public string levelName;
    //  public GameObject Endscrn;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(index);
            SceneManager.LoadScene(levelName);
            //Endscrn.gameObject.SetActive(true);

        }
    }

}
