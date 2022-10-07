using UnityEngine;

namespace UI.AnimationButtons
{
    public class UIButtonAnimation : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Highlighted = Animator.StringToHash("Highlighted");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void OnMouseEnter()
        {
            _animator.SetBool(Highlighted,true);
        }

        private void OnMouseExit()
        {
            _animator.SetBool(Highlighted,false);
        }
    }
}