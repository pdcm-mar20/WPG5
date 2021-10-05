using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    private string nameExist, pwdExist;
    [SerializeField] private InputField name, pwd;
    void Start()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("Users")
            .ValueChanged += GetUserExist;
    }

    public void Enter()
    {
        print("pwd "+pwdExist);
        if (name.text == nameExist)
        {
            if (pwd.text == pwdExist)
            {
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
                var n = childSnapshot.Child("name").Value.ToString();
                var pwd = childSnapshot.Child("pwd").Value.ToString();
                nameExist = n;
                pwdExist = pwd;
                Debug.Log(n);
            }
        }
    }

    public void GotoRegister()
    {
        SceneManager.LoadScene("Register");
    }
}
