using UnityEngine;

namespace UI.AnimationButtons
{
    public class UIStartButtonAnimation : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private GameObject rawEnemyImage;
        private static readonly int Highlighted = Animator.StringToHash("Highlighted");
        private static readonly int HelpMessage = Animator.StringToHash("HelpMessage");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void OnMouseEnter()
        {
            _animator.SetBool(rawEnemyImage.activeInHierarchy ? Highlighted : HelpMessage, true);
        }

        private void OnMouseExit()
        {
            _animator.SetBool(rawEnemyImage.activeInHierarchy ? Highlighted : HelpMessage, false);
        }
    }
}