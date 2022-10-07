using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

namespace Game
{
    public static class Player
    {
        public static DatabaseReference Reference;
        public static FirebaseAuth Auth;
        public static FirebaseUser User;
        public static string DollIndex;
        public static string EnemyName;
        public static Texture EnemyImage;

        public static string UserId => null;
    }
}