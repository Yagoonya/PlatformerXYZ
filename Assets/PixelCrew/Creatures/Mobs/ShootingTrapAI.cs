using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [Header("Range")] [SerializeField] private SpawnComponent _rangeAttack;
        [SerializeField] protected Cooldown _rangeCooldown;
        [SerializeField] public ColliderCheck _vision;

        private static readonly int Range = Animator.StringToHash("range");

        protected Animator Animator;

        protected virtual void Update()
        {
            if (_vision.IsTouchingLayer && _rangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        public void RangeAttack()
        {
            _rangeCooldown.Reset();
            Animator.SetTrigger(Range);
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}