using Player;
using UnityEngine;

public class WaterBallActive : MonoBehaviour
{
    [SerializeField] private GameObject waterBall;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.D) && DataItems.waterBall > 0)
        {
            DataItems.waterBall -= 1;
            Instantiate(waterBall, transform.position, Quaternion.identity);
        }
    }
}