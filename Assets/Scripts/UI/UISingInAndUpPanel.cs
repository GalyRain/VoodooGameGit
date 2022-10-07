using System.Collections;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Game;
using UnityEngine;
using TMPro;

namespace UI
{
    public class UISingInAndUpPanel : MonoBehaviour
    {
        [Header("SingInAndUpPanel")]
        [SerializeField] private GameObject singInAndUpPanel;
        [SerializeField] private GameObject startPanel;
        [SerializeField] private GameObject gameBlock;
        
        [SerializeField] private GameObject singInPanel;
        [SerializeField] private GameObject singUnPanel;
        
        [SerializeField] private DependencyStatus dependencyStatus;
        private DatabaseReference _reference;
        private FirebaseAuth _auth;
        private FirebaseUser _user;
        
        [Header("SingIn")]
        [SerializeField] private TMP_InputField emailSingInField;
        [SerializeField] private TMP_InputField passwordSingInField;
        [SerializeField] private TMP_Text warningSingInText;
        [SerializeField] private TMP_Text confirmSingInText;
        
        [Header("SingUp")]
        [SerializeField] private TMP_InputField emailSingUpField;
        [SerializeField] private TMP_InputField passwordSingUpField;
        [SerializeField] private TMP_InputField passwordSingUpVerifyField;
        [SerializeField] private TMP_Text warningSingUpText;

        private UIUploadImagePanel _uploadImagePanel;
        private UISelector _selector;
        
        private void Start()
        {
            _uploadImagePanel = GetComponent<UIUploadImagePanel>();
            _selector = GetComponent<UISelector>();

            StartCoroutine(CheckAndFixDependenciesAsync());
        }

        private IEnumerator CheckAndFixDependenciesAsync()
        {
            var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();

            yield return new WaitUntil(() => dependencyTask.IsCompleted);
            
            dependencyStatus = dependencyTask.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
                yield return new WaitForEndOfFrame();
                StartCoroutine(CheckAForAutoSingIn());
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        }
        
        private void InitializeFirebase()
        {
            _auth = FirebaseAuth.DefaultInstance;
            _reference = FirebaseDatabase.DefaultInstance.RootReference;
            _auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
            
            Player.Auth = _auth;
            Player.Reference = _reference;
            Debug.Log("Setting up Firebase Auth");
        }
        
        private void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (_auth.CurrentUser != _user)
            {
                bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null;
                if (!signedIn && _user != null)
                {
                    Debug.Log("Signed out " + _user.UserId);
                }
                _user = _auth.CurrentUser;
                if (signedIn)
                {
                    Debug.Log("Signed in " + _user.UserId);
                    Player.User = _user;
                }
            }
        }

        private IEnumerator CheckAForAutoSingIn()
        {
            if (_user != null)
            {
                var reloadUserTask = _user.ReloadAsync();

                yield return new WaitUntil(() => reloadUserTask.IsCompleted);
                
                SetStartPanel();
                StartCoroutine(LoadDataUser());
            }
            else
            {
                singInAndUpPanel.SetActive(true);
            }
        }

        public void ChooseSingIn()
        {
            singUnPanel.SetActive(false);
            singInPanel.SetActive(true);
        }
        
        public void ChooseSingUp()
        {
            singInPanel.SetActive(false);
            singUnPanel.SetActive(true);
        }

        public void SingInButton()
        {
            StartCoroutine(SingIn(emailSingInField.text, passwordSingInField.text));
        }
        
        public void SingUpButton()
        {
            StartCoroutine(SingUp(emailSingUpField.text, passwordSingUpField.text));
        }
        
        private void ClearSingInFields()
        {
            emailSingInField.text = "";
            passwordSingInField.text = "";
        }
        
        private void ClearSingUpFields()
        {
            emailSingUpField.text = "";
            passwordSingUpField.text = "";
            passwordSingUpVerifyField.text = "";
        }
        
        private IEnumerator SingIn(string email, string password) 
        {
            var loginTask = _auth.SignInWithEmailAndPasswordAsync(email, password); 
            
            yield return new WaitUntil(predicate: () => loginTask.IsCompleted);
            if (loginTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {loginTask.Exception}");
                if (loginTask.Exception.GetBaseException() is FirebaseException firebaseEx)
                {
                    var errorCode = (AuthError)firebaseEx.ErrorCode;
 
                    var message = "Login Failed!";
                    switch (errorCode)
                    {
                        case AuthError.MissingEmail:
                            message = "Missing Email";
                            break;
                        case AuthError.MissingPassword:
                            message = "Missing Password";
                            break;
                        case AuthError.WrongPassword:
                            message = "Wrong Password";
                            break;
                        case AuthError.InvalidEmail:
                            message = "Invalid Email";
                            break;
                        case AuthError.UserNotFound:
                            message = "Account does not exist";
                            break;
                    }
                    warningSingInText.text = message;
                }
            }
            else 
            {
                _user = loginTask.Result;
                Player.User = _user;
                Debug.LogFormat("User signed in successfully: {0} ({1})", _user.DisplayName, _user.Email);
                warningSingInText.text = "";
                confirmSingInText.text = "Logged In";

                _uploadImagePanel.LoadButton();
                _selector.LoadButton();
 
                yield return new WaitForSeconds(2);
                
                confirmSingInText.text = "";
                ClearSingInFields();
                ClearSingUpFields();
                SetStartPanel();
            }
        }
        
        private IEnumerator SingUp(string email, string password, string username = "user")
        {
            if (username == "")
            {
                warningSingUpText.text = "Enter Name";
            }
            else if (passwordSingUpField.text != passwordSingUpVerifyField.text)
            {
                warningSingUpText.text = "Password Does Not Match!";
            }
            else 
            {
                var registerTask = _auth.CreateUserWithEmailAndPasswordAsync(email, password);
                
                yield return new WaitUntil(predicate: () => registerTask.IsCompleted);
                if (registerTask.Exception != null)
                {
                    Debug.LogWarning(message: $"Failed to register task with {registerTask.Exception}");
                    var firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
                    var errorCode = (AuthError)firebaseEx!.ErrorCode;
 
                    var message = "Register Failed!";
                    switch (errorCode)
                    {
                        case AuthError.MissingEmail:
                            message = "Missing Email";
                            break;
                        case AuthError.MissingPassword:
                            message = "Missing Password";
                            break;
                        case AuthError.WeakPassword:
                            message = "Weak Password";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            message = "Email Already In Use";
                            break;
                    }
                    warningSingUpText.text = message;
                }
                else
                {
                    _user = registerTask.Result;
                    if (_user != null)
                    {
                        var profile = new UserProfile{DisplayName = username};

                        var profileTask = _user.UpdateUserProfileAsync(profile);
                        
                        yield return new WaitUntil(predicate: () => profileTask.IsCompleted);
 
                        if (profileTask.Exception != null)
                        {
                         Debug.LogWarning(message: $"Failed to register task with {profileTask.Exception}");
                         var firebaseEx = profileTask.Exception.GetBaseException() as FirebaseException;
                         var errorCode = (AuthError)firebaseEx!.ErrorCode;
                         warningSingUpText.text = "Username Set Failed!";
                         Debug.Log(errorCode);
                        }
                        else
                        {
                            warningSingUpText.text = "";
                            ClearSingInFields();
                            ClearSingUpFields();
                            ChooseSingIn();
                        }
                        warningSingInText.text = "";
                        confirmSingInText.text = "";
                    }
                }
            }
        }

        private void SetStartPanel()
        {
            singInAndUpPanel.SetActive(false);
            startPanel.SetActive(true);
            gameBlock.SetActive(true);
        }

        private IEnumerator LoadDataUser()
        {
            _uploadImagePanel.LoadButton();
            _selector.LoadButton();
 
            yield return new WaitForSeconds(2);
        }
    }
}
