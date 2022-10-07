using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoadDataScene : MonoBehaviour
    {
        [SerializeField] private GameObject[] voodooDolls;
        private int _dollIndex;
        [SerializeField] private TMP_Text enemyName;
        [SerializeField] private RawImage rawEnemyImage;
        
        private void Start()
        {
            foreach (var go in voodooDolls)
            {
                go.SetActive(false);
            }
            
            _dollIndex = Convert.ToInt32(Player.DollIndex);
            voodooDolls[_dollIndex].SetActive(true);

            enemyName.text = Player.EnemyName;
            
            // var texture = new Texture2D(10, 10);
            // texture.LoadImage(Player.EnemyImage);
            rawEnemyImage.texture = Player.EnemyImage;

        }
    }
}
