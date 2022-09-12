using PixelCrew.Components.GameObjectBased;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [Header("Range")]
        [SerializeField] private SpawnComponent _rangeAttack;

        private static readonly int Range = Animator.StringToHash("range");

        protected Animator Animator;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        protected virtual void RangeAttack()
        {
            Animator.SetTrigger(Range);
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}