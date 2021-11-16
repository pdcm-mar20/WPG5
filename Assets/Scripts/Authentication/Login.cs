using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Authentication
{
    public class Login : MonoBehaviour
    {
        private string nameExist, pwdExist;
        [SerializeField] private InputField name, pwd;

        public void Enter()
        {
            FirebaseDatabase.DefaultInstance
                .GetReference("Users")
                .ValueChanged += GetUserExist;
        }

        private void GetUserExist(object sender2, ValueChangedEventArgs e2)
        {
            if (e2.DatabaseError != null)
            {
                Debug.LogError(e2.DatabaseError.Message);
            }

            if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
            {
                foreach (var childSnapshot in e2.Snapshot.Children)
                {
                    nameExist = childSnapshot.Child("name").Value.ToString();
                    pwdExist = childSnapshot.Child("password").Value.ToString();

                    Debug.Log(nameExist);
                    Debug.Log(name.text);
                    if (name.text == nameExist)
                    {
                        if (pwd.text == pwdExist)
                        {
                            //     PlayerPrefs.SetString(Constant.KEY_NAME, name.text);
                            SceneManager.LoadScene("MainMenu");
                        }
                        else
                        {
                            print("Password Salah");
                        }
                    }
                    else
                    {
                        print("Name Tidak Ada!");
                    }
                }
            }
        }

        public void GotoRegister()
        {
            SceneManager.LoadScene("Register");
        }
    }
}