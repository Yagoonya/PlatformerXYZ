using UnityEngine;

namespace PixelCrew.UI.Windows
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            
            _animator.SetTrigger(Show);
        }

        public virtual void Close()
        {
            _animator.SetTrigger(Hide);
        }

        protected virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}