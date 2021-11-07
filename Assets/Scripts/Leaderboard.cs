using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public GameObject item;

    private void Start()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("Users").OrderByChild("score").LimitToLast(4) 
            .ValueChanged += GetLeaderBoard;
    }

    private void GetLeaderBoard(object sender2, ValueChangedEventArgs e2)
    {
        if (e2.DatabaseError != null)
        {
            Debug.LogError(e2.DatabaseError.Message);
        }

        if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
        {
            var i = 1;
            foreach (var childSnapshot in e2.Snapshot.Children)
            {
                var go = Instantiate(item, new Vector3(0, 0, 0), Quaternion.identity);
                go.transform.parent = GameObject.Find("Panel").transform;
                go.transform.localPosition = new Vector2(55, -40 * i);
                go.transform.localScale = new Vector3(1, 1, 1);

                var no = go.transform.GetChild(0).gameObject;
                no.GetComponent<Text>().text = i.ToString();
                var name = go.transform.GetChild(1).gameObject;
                name.GetComponent<Text>().text = childSnapshot.Child("name").Value.ToString();
                var score = go.transform.GetChild(2).gameObject;
                score.GetComponent<Text>().text = childSnapshot.Child("score").Value.ToString();

                i++;
            }
        }
    }
}