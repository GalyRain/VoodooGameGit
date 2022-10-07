using System;
using Game;
using UnityEngine;

namespace UI.GameAnimation
{
    public class UIGameAnimation : MonoBehaviour
    {
        private static readonly int HitFromLeft = Animator.StringToHash("Hit_from_left");
        private static readonly int HitFromRight = Animator.StringToHash("Hit_from_right");
        private static readonly int HitFromFront = Animator.StringToHash("Hit_from_front");
        private static readonly int HitFromBack = Animator.StringToHash("Hit_from_back");
        private static readonly int DeathLeft = Animator.StringToHash("Death_left");
        private static readonly int DeathRight = Animator.StringToHash("Death_right");
        private static readonly int DeathForward = Animator.StringToHash("Death_forward");
        private static readonly int DeathBackward = Animator.StringToHash("Death_backward");
        
        [SerializeField] private GameObject[] voodooDolls;
        private int _dollIndex;
        private Animator _animator;

        private void Start()
        {
            _dollIndex = Convert.ToInt32(Player.DollIndex);
            _animator = voodooDolls[_dollIndex].GetComponent<Animator>();
        }

        public void TapHitLeftButton() => _animator.SetTrigger(HitFromLeft);

        public void TapHitRightButton() => _animator.SetTrigger(HitFromRight);
        
        public void TapHitFrontButton() => _animator.SetTrigger(HitFromFront);
        
        public void TapHitBackButton() => _animator.SetTrigger(HitFromBack);
        
        public void TapDeathLeftButton() => _animator.SetTrigger(DeathLeft);
        
        public void TapDeathRightButton() => _animator.SetTrigger(DeathRight);
        
        public void TapDeathForwardButton() => _animator.SetTrigger(DeathForward);
        
        public void TapDeathBackwardButton() => _animator.SetTrigger(DeathBackward);
    }
}
