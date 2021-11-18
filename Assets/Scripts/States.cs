using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class States : MonoBehaviour
{

    [SerializeField]private GameObject name;
    
    void Start()
    {
        Debug.Log("Size "+Finish.GetListWinner().Count);
        Debug.Log("Name "+Finish.GetListWinner()[0]);
      
        for (int i = 0; i < Finish.GetListWinner().Count; i++)
        {
            var go = Instantiate(name, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.parent = GameObject.Find("Canvas").transform;
            go.transform.localPosition = new Vector2(-140, 85 -  (i * 50));
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<Text>().text = Finish.GetListWinner()[i];
            
            Debug.Log("Finish "+i+1 + " " + Finish.GetListWinner()[i]);
        }
        
        Destroy(GameObject.FindWithTag("Finish"));
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
}