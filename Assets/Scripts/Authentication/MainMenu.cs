using System;
using System.Collections;
using DefaultNamespace;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Authentication
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Text nameText;
        private string name;
        private DataUser user;

        [SerializeField] private RawImage image;

        void Start()
        {
            PlayerPrefs.SetInt("isRegistered", 1);
            name = PlayerPrefs.GetString(Constant.KEY_NAME);
            FirebaseDatabase.DefaultInstance
                .GetReference("Users")
                .ValueChanged += GetUser;
        }

        private void GetUser(object sender2, ValueChangedEventArgs e2)
        {
            if (e2.DatabaseError != null)
            {
                Debug.LogError(e2.DatabaseError.Message);
            }

            if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
            {
                foreach (var childSnapshot in e2.Snapshot.Children)
                {
                    var n = childSnapshot.Child("name").Value.ToString();
                    if (n == name)
                    {
                        SetValueUser(childSnapshot);
                    }
                }
            }
        }

        private void SetValueUser(DataSnapshot snapshot)
        {
            user.name = snapshot.Child("name").Value.ToString();
            user.score = Convert.ToUInt32(snapshot.Child("score").Value.ToString());
            user.coin = Convert.ToUInt32(snapshot.Child("coin").Value.ToString());
            user.password = snapshot.Child("password").Value.ToString();
            user.character1 = Convert.ToBoolean(snapshot.Child("character1").Value.ToString());
            user.character2 = Convert.ToBoolean(snapshot.Child("character2").Value.ToString());

            SetName(name);
        }
        
        private void SetName(string name)
        {
            nameText.text = name;
        }

        public void Play()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void EditProfile()
        {
            SceneManager.LoadScene("EditProfile");
        }

        public void Leaderboard()
        {
            SceneManager.LoadScene("Leaderboard");
        }

        public void Credits()
        {
            SceneManager.LoadScene("Credit");
        }

        public void Shop()
        {
            SceneManager.LoadScene("Shop");
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}