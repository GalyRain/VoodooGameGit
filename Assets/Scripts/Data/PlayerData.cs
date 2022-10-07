using Game;

namespace Data
{
    [System.Serializable]
    public class PlayerData
    {
        public string dollIndexData;
        
        public PlayerData(PlayerGame player)
        {
            dollIndexData = player.dollIndexGame;
        }
    }
}