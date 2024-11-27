using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ImageDownloadUI : MonoBehaviour
{
    #region PRIVATE_VARS
    [SerializeField] private TMP_InputField imageURPField;
    [SerializeField] private Button downloadButton;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private VisualEffect vfxEffect;
    #endregion

    #region UNITY_CALLBACKS
    private void OnEnable()
    {
        downloadButton.onClick.AddListener(OnDownloadClicked);
    }
    private void OnDisable()
    {
        downloadButton.onClick.RemoveListener(OnDownloadClicked);
    }
    #endregion

    #region PRIVATE_METHODS
    private void OnDownloadClicked()
    {
        if (imageURPField == null || string.IsNullOrWhiteSpace(imageURPField.text))
        {
            Debug.LogError("URL Input Field is empty or invalid.");
            return;
        }

        if (IsValidImageURL(imageURPField.text))
        {
            imageURPField.interactable = false;
            downloadButton.interactable = false;

            ImageDownloader.instance.DownloadImage(imageURPField.text, OnImageDownloaded);
        }
        else
        {
            Debug.LogError("Invalid URL. Only PNG and JPG images are supported.");
        }
    }

    private void OnImageDownloaded(Texture2D downloadedTexture)
    {
        if (downloadedTexture != null)
        {
            if (rawImage != null)
            {
                rawImage.texture = downloadedTexture;
                vfxEffect.SetTexture("MainTex", downloadedTexture);
            }
        }
        else
        {
            Debug.LogError("Failed to download the image.");
        }

        imageURPField.interactable = true;
        downloadButton.interactable = true;
    }

    private bool IsValidImageURL(string url)
    {
        return url.EndsWith(".png", System.StringComparison.OrdinalIgnoreCase) || url.EndsWith(".jpg", System.StringComparison.OrdinalIgnoreCase) || url.EndsWith(".jpeg", System.StringComparison.OrdinalIgnoreCase);
    }
    #endregion
}
