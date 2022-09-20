using PixelCrew.Components.ColliderBased;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class SeaShellAI : ShootingTrapAI
    {
        [Header("Melee")]
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverlap _melleAttack;
        [SerializeField] private ColliderCheck _canMeleeAttack;
        
        private static readonly int Melee = Animator.StringToHash("melee");
        
        protected override void Update()
        {
            {
                if (_vision.IsTouchingLayer)
                {
                    if (_canMeleeAttack.IsTouchingLayer)
                    {
                        if(_meleeCooldown.IsReady)
                            MeleeAttack();
                        return;
                    }

                    if (_rangeCooldown.IsReady)
                        RangeAttack();
                }
            }
        }
        
        private void MeleeAttack()
        {
            _meleeCooldown.Reset();
            Animator.SetTrigger(Melee);
        }
        
        public void OnMeleeAttack()
        {
            _melleAttack.Check();
        }
    }
}