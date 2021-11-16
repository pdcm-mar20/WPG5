using UnityEngine;

public class States : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Size "+Finish.GetListWinner().Count);
        Debug.Log("Name "+Finish.GetListWinner()[0]);
        for (int i = 0; i < Finish.GetListWinner().Count; i++)
        {
            Debug.Log("Finish "+i+1 + " " + Finish.GetListWinner()[i]);
        }
        
        Destroy(GameObject.FindWithTag("Finish"));
    }
}