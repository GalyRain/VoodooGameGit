using System;
using System.Collections;
using Game;
using UnityEngine;

namespace UI
{
    public class UISelector : MonoBehaviour
    {
        [SerializeField] private GameObject[] voodooDolls;
        private int _dollIndex;

        private void Start()
        {
            foreach (var go in voodooDolls)
            {
                go.SetActive(false);
            }

            if (voodooDolls[0])
            {
                voodooDolls[0].SetActive(true);
            }
        }
        
        public void PreviousButton()
        {
            voodooDolls[_dollIndex].SetActive(false);
            _dollIndex--;
            if (_dollIndex < 0)
            {
                _dollIndex = voodooDolls.Length - 1;
            }
            voodooDolls[_dollIndex].SetActive(true);

            Player.DollIndex = _dollIndex.ToString();
            StartCoroutine(UpdateDollIndexDatabase(Player.DollIndex));
        }

        public void NextButton()
        {
            voodooDolls[_dollIndex].SetActive(false);
            _dollIndex++;
            if (_dollIndex == voodooDolls.Length)
            {
                _dollIndex = 0;
            }
            voodooDolls[_dollIndex].SetActive(true);
            
            Player.DollIndex = _dollIndex.ToString();
            StartCoroutine(UpdateDollIndexDatabase(Player.DollIndex));
        }
        
        private IEnumerator UpdateDollIndexDatabase(string indexDoll)
        {
            var dbTask = Player.Reference.Child("users").Child(Player.User.UserId).Child("indexDoll").SetValueAsync(indexDoll);
        
            yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
        
            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
        }
        
        private IEnumerator LoadDollIndex()
        {
            var dbTask = Player.Reference.Child("users").Child(Player.User.UserId).GetValueAsync();
        
            yield return new WaitUntil(predicate: () => dbTask.IsCompleted);
        
            if (dbTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {dbTask.Exception}");
            }
            else if (dbTask.Result.Value == null)
            {
                Player.DollIndex = "0";
                StartCoroutine(UpdateDollIndexDatabase(Player.DollIndex));
            }
            else
            {
                Player.DollIndex = dbTask.Result.Child("indexDoll").Value.ToString();
                _dollIndex = Convert.ToInt32(Player.DollIndex);
            
                foreach (GameObject go in voodooDolls)
                {
                    go.SetActive(false);
                }
            
                voodooDolls[_dollIndex].SetActive(true);
            }
        }
        
        public void LoadButton()
        {
            StartCoroutine(LoadDollIndex());
        }
    }
}