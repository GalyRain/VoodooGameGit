using Game;
using UnityEngine;

namespace Data
{
    public class PlayerGame : MonoBehaviour
    {
        public string dollIndexGame;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveDataPlayer();
                Debug.Log("Save" + dollIndexGame);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadDataPlayer();
                Debug.Log("Load" + dollIndexGame);
            }
            
            dollIndexGame = Player.UserId;
        }
        
        private void SaveDataPlayer()
        {
            Storage.SaveDataPlayer(this);
        }
        
        private void LoadDataPlayer()
        {
            var game = Storage.LoadDataPlayer();
            dollIndexGame = game.dollIndexData;
        }
    }
}