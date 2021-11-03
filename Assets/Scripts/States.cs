using UnityEngine;

public class States : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < Finish.GetListWinner().Count; i++)
        {
            Debug.Log(i+1 + " " + Finish.GetListWinner()[i]);
        }
    }
}