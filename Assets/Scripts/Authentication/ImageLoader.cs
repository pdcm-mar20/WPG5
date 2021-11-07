using System;
using System.Collections;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Authentication
{
    public class ImageLoader : MonoBehaviour
    {
        private RawImage image;
        private FirebaseStorage storage;

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

        private void Start()
        {
            image = GetComponent<RawImage>();
            //     StartCoroutine(LoadImage("https://firebasestorage.googleapis.com/v0/b/oceanrunv2.appspot.com/o/bg2.png?alt=media&token=b0a06899-6ea6-449b-9ed5-6366070cbf54"));
            var reference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://oceanrunv2.appspot.com/");
            var img = reference.Child("bg2.png");
            img.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
            {
                if (!task.IsCanceled && !task.IsFaulted)
                {
                    StartCoroutine(LoadImage(Convert.ToString(task.Result)));
                    Debug.Log("Result "+task.Result);
                }
                else
                {
                    Debug.Log(task.Exception);
                }
            });
        }
    }
}