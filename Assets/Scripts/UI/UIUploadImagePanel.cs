using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using Firebase.Extensions;
using Game;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.Windows;

namespace UI
{
    public class UIUploadImagePanel : MonoBehaviour
    {
        [Header("UploadImagePanel")]
        [SerializeField] private GameObject uploadImagePanel;
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject gameBlock;
        
        [SerializeField] private GameObject enemyTextGameObject;
        [SerializeField] private GameObject uploadEnemyTextGameObject;
        
        [SerializeField] private GameObject rawImageGameObject;
        [SerializeField] private GameObject uploadRawImageGameObject;
        
        [SerializeField] private RawImage rawEnemyImage;
        [SerializeField] private RawImage uploadRawEnemyImage;
        
        [SerializeField] private TMP_Text enemyNameText;
        [SerializeField] private TMP_InputField enemyNameField;
        
        private Texture2D _texture;
        private FirebaseStorage _storage;
        private StorageReference _storageReference;
        private byte[] _bytes;
        private const string StorageName = "gs://voodoogame-9c0dc.appspot.com/";

        private void Start()
        {
            _storage = FirebaseStorage.DefaultInstance;
        }

        public void EnemyImage()
        {
            uploadImagePanel.SetActive(true);
            startPanel.SetActive(false);
            gameBlock.SetActive(false);
        }

        public void ConfirmButton()
        {
            SetValue();
            if (!uploadEnemyTextGameObject.activeSelf)
            {
                SetImage();
                UploadImage();
            }
            StartCoroutine(UpdateEnemyName(enemyNameField.text));
            StartCoroutine(LoadUserEnemyName());
        }
        
        public void ClosedButton()
        {
            SetValue();
        }

        private void SetValue()
        {
            uploadImagePanel.SetActive(false);
            startPanel.SetActive(true);
            gameBlock.SetActive(true);
        }

        private void SetImage()
        {
            rawEnemyImage.texture = _texture;
            enemyTextGameObject.SetActive(false);
            rawImageGameObject.SetActive(true);
        }
        
        private void UploadPanelSetImage()
        {
            uploadRawEnemyImage.texture = _texture;
            uploadEnemyTextGameObject.SetActive(false);
            uploadRawImageGameObject.SetActive(true);
        }

        public void UploadImageFromGallery(int maxSize = 1024)
        {
            NativeGallery.GetImageFromGallery((path) =>
            {
                Debug.Log("Image path: " + path);
                if (path != null)
                {
                    _texture = NativeGallery.LoadImageAtPath(path, 2048);
                    Debug.Log("Good Upload");
                    if (_texture == null)
                    {
                        Debug.Log("Couldn't load texture from " + path);
                        return;
                    }

                    UploadPanelSetImage();
                    
                    _bytes = File.ReadAllBytes(path);
                    
                    Player.EnemyImage = _texture;
                }
            });
        }

        private void UploadImage()
        {
            var uploadReference  = _storage.RootReference.Child("enemyImage").Child(Player.User.UserId);
            
            var newMetaData = new MetadataChange { ContentType = "image/png" };
            
            uploadReference.PutBytesAsync(_bytes, newMetaData).ContinueWith(task =>
            {
                if (task.IsCompleted || task.IsCanceled)
                {
                    if (task.Exception != null) Debug.Log(task.Exception.ToString());
                }
                else
                {
                    Debug.Log("File Uploaded Successfully!");
                }
            });
        }

        private IEnumerator LoadImageUrl(string imageUrl)
        {
            var webRequest = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return webRequest.SendWebRequest();
            _texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            
            UploadPanelSetImage();
            SetImage();
        }

        private void LoadImage()
        {
            _storageReference = _storage.GetReferenceFromUrl(StorageName);
            var enemyImage = _storageReference.Child("enemyImage").Child(Player.User.UserId);
            enemyImage.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
            {
                if (!task.IsFaulted && !task.IsCanceled)
                {
                    StartCoroutine(LoadImageUrl(Convert.ToString(task.Result)));
                    Player.EnemyImage = _texture;
                }
                else
                {
                    Debug.Log(task.Exception);
                }
            });
        }

        private IEnumerator LoadUserEnemyName()
        {
            var dbTask = Player.Reference.Child("users").Child(Player.User.UserId).GetValueAsync();
        
            yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
        
            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
            else if (dbTask.Result.Value == null)
            {
                Debug.Log(null);
                enemyNameText.text = "";
            }
            else
            {
                enemyNameText.text = dbTask.Result.Child("enemyName").Value.ToString();
                enemyNameField.text = dbTask.Result.Child("enemyName").Value.ToString();
                Player.EnemyName = enemyNameText.text;
            }
        }
        
        private IEnumerator UpdateEnemyName(string enemyName)
        {
            Player.EnemyName = enemyNameField.text;
            
            var dbTask = Player.Reference.Child("users").Child(Player.User.UserId).Child("enemyName").SetValueAsync(enemyName);
        
            yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
        
            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
        }

        public void LoadButton()
        {
            StartCoroutine(LoadUserEnemyName());
            LoadImage();
        }
    }
}