using Firebase.Auth;
using UnityEngine;

namespace UI
{
    public class UISingInApple : MonoBehaviour
    {
        private FirebaseAuth _auth;
        private string _appleIdToken;
        private string _rawNonce;

        public void SingInApple()
        {
            var credential = Firebase.Auth.OAuthProvider.GetCredential("apple.com", _appleIdToken, _rawNonce, null);
        }

    }
}
