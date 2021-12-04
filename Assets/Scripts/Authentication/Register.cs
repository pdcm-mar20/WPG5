using System;
using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace Authentication
{
    public class Register : MonoBehaviour
    {
        private DatabaseReference reference;

        [SerializeField] public new InputField name;
        [SerializeField] public InputField pwd;
        private string nameExist = "";
        private byte[] bytes;
        private string destinationPath;
        private string nameImg;
        
        [Header("Error Field")] 
        public Text errName;
        public Text errPwd;

        private void Awake()
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        private void Start()
        {
            FirebaseDatabase.DefaultInstance
                .GetReference("Users")
                .ValueChanged += GetNameExist;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.C))
            {
                PlayerPrefs.DeleteAll();
            }
        }

        private void GetNameExist(object sender2, ValueChangedEventArgs e2)
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

                    nameExist = n;
                    Debug.Log(n);
                }
            }
        }

        public void Regist()
        {
            if (NameCheck() && PwdCheck())
            {
                var id = UserId();
                var user = new DataUser(id,name.text, pwd.text, 0, 0, false, false);
                var json = JsonUtility.ToJson(user);
                reference.Child("Users").Child(id).SetRawJsonValueAsync(json);
                print("Success");
                PlayerPrefs.SetString(Constant.KEY_NAME, name.text);
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                print("Gagal");
            }
        }

        public bool PwdCheck()
        {
            if (pwd != null && pwd.text != "")
            {
                errPwd.text = "";
                return true;
            }

            var message = "Password Harus Diisi!";
            errPwd.text = message;
            print(message);

            return false;
        }

        public bool NameCheck()
        {
            if (name != null && name.text != "" && nameExist != name.text)
            {
                errName.text = "";
                return true;
            }

            else if (nameExist == name.text)
            {
                var message = "Username Sudah Ada!";
                print(message);
                errName.text = message;
                return false;
            }
            else
            {
                var message = "Username Harus Diisi!";
                print(message);
                errName.text = message;
                return false;
            }
        }


        public string UserId()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        public void GoToLogin()
        {
            SceneManager.LoadScene("Login");
        }
    }
}