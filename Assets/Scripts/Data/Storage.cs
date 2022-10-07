using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Data
{
    public static class Storage
    {
        private static readonly string Path = Application.persistentDataPath + "/PlayerData.save";
        
        public static void SaveDataPlayer(PlayerGame game)
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(Path, FileMode.Create);


            var playerData = new PlayerData(game);

            formatter.Serialize(stream, playerData);
            stream.Close();
        }

        public static PlayerData LoadDataPlayer()
        {
            if (File.Exists(Path))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(Path, FileMode.Open);

                var playerData = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return playerData;
            }
            Debug.LogError("Save file not found in" + Path);
            return null;
        }
    }
}