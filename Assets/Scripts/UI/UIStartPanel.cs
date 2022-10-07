using UnityEngine;
using UnityEngine.SceneManagement;
using Game;

namespace UI
{
    public class UIStartPanel : MonoBehaviour
    {
        [Header("StartPanel")]
        [SerializeField] private GameObject gameBlock;
        [SerializeField] private GameObject starPanel;
        [SerializeField] private GameObject singInAndUpPanel;
        [SerializeField] private GameObject rawEnemyImage;

        public void ExitButton()
        {
            Player.Auth.SignOut();
            starPanel.SetActive(false);
            gameBlock.SetActive(false);
            singInAndUpPanel.SetActive(true);
        }

        public void StartButton()
        {
            if (rawEnemyImage.activeInHierarchy) SceneManager.LoadScene(sceneBuildIndex: 1);
        }
        
        public void BackButton()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    }
}
