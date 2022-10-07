using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using UnityEngine;
using TMPro;

namespace UI
{
    public class UISingInGoogle : MonoBehaviour
    {
         
    // [SerializeField] public TMP_Text infoText;
    // private string webClientId = "621600787870-o08gq76da192pe2tj1pt9j1a48139tv0.apps.googleusercontent.com";
    //
    // private FirebaseAuth _auth;
    // private GoogleSignInConfiguration _configuration;
    //
    // private void Awake()
    // {
    //     _configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
    //     // CheckFirebaseDependencies();
    // }
    //
    // private void CheckFirebaseDependencies()
    // {
    //     FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
    //     {
    //         if (task.IsCompleted)
    //         {
    //             if (task.Result == DependencyStatus.Available)
    //                 _auth = FirebaseAuth.DefaultInstance;
    //             else
    //                 AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
    //         }
    //         else
    //         {
    //             if (task.Exception != null)
    //                 AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
    //         }
    //     });
    // }
    //
    // public void SignInWithGoogle() { OnSignIn(); }
    // public void SignOutFromGoogle() { OnSignOut(); }
    //
    // private void OnSignIn()
    // {
    //     GoogleSignIn.Configuration = _configuration;
    //     GoogleSignIn.Configuration.UseGameSignIn = false;
    //     GoogleSignIn.Configuration.RequestIdToken = true;
    //     AddToInformation("Calling SignIn");
    //
    //     GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    // }
    //
    // private void OnSignOut()
    // {
    //     AddToInformation("Calling SignOut");
    //     GoogleSignIn.DefaultInstance.SignOut();
    // }
    //
    // public void OnDisconnect()
    // {
    //     AddToInformation("Calling Disconnect");
    //     GoogleSignIn.DefaultInstance.Disconnect();
    // }
    //
    // private void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    // {
    //     if (task.IsFaulted)
    //     {
    //         if (task.Exception != null)
    //             using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
    //             {
    //                 if (enumerator.MoveNext())
    //                 {
    //                     GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
    //                     AddToInformation("Got Error: " + error.Status + " " + error.Message);
    //                 }
    //                 else
    //                 {
    //                     AddToInformation("Got Unexpected Exception?!?" + task.Exception);
    //                 }
    //             }
    //     }
    //     else if (task.IsCanceled)
    //     {
    //         AddToInformation("Canceled");
    //     }
    //     else
    //     {
    //         AddToInformation("Welcome: " + task.Result.DisplayName + "!");
    //         AddToInformation("Email = " + task.Result.Email);
    //         AddToInformation("Google ID Token = " + task.Result.IdToken);
    //         AddToInformation("Email = " + task.Result.Email);
    //         SignInWithGoogleOnFirebase(task.Result.IdToken);
    //     }
    // }
    //
    // private void SignInWithGoogleOnFirebase(string idToken)
    // {
    //     Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
    //
    //     _auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
    //     {
    //         AggregateException ex = task.Exception;
    //         if (ex != null)
    //         {
    //             if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
    //                 AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
    //         }
    //         else
    //         {
    //             AddToInformation("Sign In Successful.");
    //         }
    //     });
    // }
    //
    // public void OnSignInSilently()
    // {
    //     GoogleSignIn.Configuration = _configuration;
    //     GoogleSignIn.Configuration.UseGameSignIn = false;
    //     GoogleSignIn.Configuration.RequestIdToken = true;
    //     AddToInformation("Calling SignIn Silently");
    //
    //     GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    // }
    //
    // public void OnGamesSignIn()
    // {
    //     GoogleSignIn.Configuration = _configuration;
    //     GoogleSignIn.Configuration.UseGameSignIn = true;
    //     GoogleSignIn.Configuration.RequestIdToken = false;
    //
    //     AddToInformation("Calling Games SignIn");
    //
    //     GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    // }
    //
    // private void AddToInformation(string str) { infoText.text +=  str; }
    }
}
