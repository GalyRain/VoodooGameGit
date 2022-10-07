using UnityEngine;

namespace UI
{
    public class UIHelpPanel : MonoBehaviour
    {
        [Header("HelpPanel")]
        [SerializeField] private GameObject helpPanel;
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject gameBlock;
        
        public void HelpButton()
        {
            helpPanel.SetActive(true);
            
            startPanel.SetActive(false);
            gameBlock.SetActive(false);
        }

        public void UnderstandAndClosedButton()
        {
            helpPanel.SetActive(false);
            
            gameBlock.SetActive(true);
            startPanel.SetActive(true);
        }
    }
}
