using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ImageDownloader : MonoBehaviour
{
    #region PUBLIC_VARS
    public static ImageDownloader instance;
    #endregion

    #region UNITY_CALLBACKS
    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region PUBLIC_METHODS
    public async void DownloadImage(string imageURL, Action<Texture2D> callback)
    {
        Texture2D image = await DownloadImageFromURL(imageURL);
        callback?.Invoke(image);
    }
    #endregion

    #region PRIVATE_METHODS
    private async Task<Texture2D> DownloadImageFromURL(string imageURL)
    {
        Texture2D image = null;
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageURL))
        {
            var asyncOperation = uwr.SendWebRequest();
            while (!asyncOperation.isDone)
            {
                await Task.Yield(); // Ensure it doesn’t block the main thread
            }

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(uwr.error);
            }
            else
            {
                image = DownloadHandlerTexture.GetContent(uwr);
            }
        }
        return image;
    }
    #endregion
}
