using System.Collections;
using System.IO;
using Firebase.Extensions;
using Firebase.Storage;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadFile : MonoBehaviour
{
    private FirebaseStorage storage;
    private StorageReference reference;
    [SerializeField]private RawImage image;
    private void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"),
            new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

        FileBrowser.SetDefaultFilter(".jpg");

        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");


        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        reference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://oceanrunv2.appspot.com/");
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
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

            // Or, copy the first file to persistentDataPath
            string destinationPath = Path.Combine(Application.persistentDataPath,
                FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);

            StartCoroutine(LoadImage(destinationPath));

            var name = FileBrowserHelpers.GetFilename(FileBrowser.Result[0]);
            var newMetaData = new MetadataChange();
            newMetaData.ContentType = "image/jpeg";
            var storageReference = reference.Child("profile/"+name);
            storageReference.PutBytesAsync(bytes, newMetaData).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.Log(task.Exception);
                }
                else
                {
                    Debug.Log("Sucess Upload");
                }
            });
        }
        
        IEnumerator LoadImage(string url)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
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

    }
}