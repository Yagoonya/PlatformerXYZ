﻿using UnityEngine;

namespace PixelCrew.Components.Interactions
{
    public class SwitchComponent : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;
        [SerializeField] private string _animationKey;


        private void Awake()
        {
            if(_state == true)
            {
                _animator.SetBool(_animationKey, _state);
            }  
        }

        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animationKey, _state);
        }


        [ContextMenu("Switch")]
        public void SwitchIt()
        {
            Switch();
        }
    }
}
