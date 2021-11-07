using System;
using System.Collections;
using System.IO;
using DefaultNamespace;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Storage;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace Authentication
{
    public class Register : MonoBehaviour
    {
        private DatabaseReference reference;
        private StorageReference storageReference;

        [SerializeField] private RawImage image;
        [SerializeField] private new InputField name;
        [SerializeField] private InputField pwd;
        private string nameExist = "";
        private byte[] bytes;
        private string destinationPath;
        private string nameImg;

        private void Awake()
        {
            reference = FirebaseDatabase.DefaultInstance.RootReference;
        }

        private void Start()
        {
            FileInit();

            FirebaseDatabase.DefaultInstance
                .GetReference("Users")
                .ValueChanged += GetNameExist;

            storageReference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://oceanrunv2.appspot.com/");
        }

        public void Choice()
        {
            StartCoroutine(ShowLoadDialogCoroutine());
        }

        IEnumerator ShowLoadDialogCoroutine()
        {
            yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null,
                "Load Files and Folders", "Load");

            // Dialog is closed
            // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
            Debug.Log(FileBrowser.Success);

            if (FileBrowser.Success)
            {
                // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
                for (int i = 0; i < FileBrowser.Result.Length; i++)
                    Debug.Log(FileBrowser.Result[i]);

                // Read the bytes of the first file via FileBrowserHelpers
                // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
                bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

                // Or, copy the first file to persistentDataPath
                destinationPath = Path.Combine(Application.persistentDataPath,
                    FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
                FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
                nameImg = FileBrowserHelpers.GetFilename(FileBrowser.Result[0]);
                StartCoroutine(LoadImage(destinationPath));
            }
        }

        IEnumerator LoadImage(string url)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                image.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            }
        }


        private void FileInit()
        {
            FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"),
                new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

            FileBrowser.SetDefaultFilter(".jpg");

            FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

            FileBrowser.AddQuickLink("Users", "C:\\Users", null);
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
                var newMetaData = new MetadataChange();
                newMetaData.ContentType = "image/jpeg";
                var storage = storageReference.Child("profile/" + nameImg);
                storage.PutBytesAsync(bytes, newMetaData).ContinueWithOnMainThread(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        Debug.Log(task.Exception);
                    }
                    else
                    {
                        var user = new DataUser(name.text, pwd.text, 0, 0,
                            "gs://oceanrunv2.appspot.com/profile/" + nameImg, false, false);
                        var json = JsonUtility.ToJson(user);
                        reference.Child("Users").Child(UserId()).SetRawJsonValueAsync(json);
                        print("Success");
                        PlayerPrefs.SetString(Constant.KEY_NAME, name.text);
                        SceneManager.LoadScene("MainMenu");
                    }
                });
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
}