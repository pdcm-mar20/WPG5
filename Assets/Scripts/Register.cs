using System;
using Firebase.Database;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Register : MonoBehaviour
{
    private DatabaseReference _reference;

    [SerializeField] private InputField name;
    [SerializeField] private InputField pwd;
    private string nameExist = "";

    private void Awake()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("Users")
            .ValueChanged += GetNameExist;
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
            var user = new DataUser(name.text, pwd.text, 0, 0);
            var json = JsonUtility.ToJson(user);
            _reference.Child("Users").Child(UserId()).SetRawJsonValueAsync(json);
            print("Success");

            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            print("Gagal");
        }
    }

    private bool PwdCheck()
    {
        if (pwd != null && pwd.text != "")
        {
            return true;
        }

        print("Password Harus Diisi");
        return false;
    }

    private bool NameCheck()
    {
        if (name == null && name.text == "")
        {
            print("Username Harus Diisi!");
            return false;
        }

        if (nameExist == name.text)
        {
            print("Username Sudah Ada!");
            return false;
        }

        return true;
    }

    private static string UserId()
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