using System.Collections;
using PixelCrew.Components.ColliderBased;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class PinkyStarAI : MobAI
    {
        [SerializeField] private float _prepareTime;
        [SerializeField] private float _idleTime;
        [SerializeField] private float _rollingSpeed;
        [SerializeField] private LayerCheck _platformChecker;
        [SerializeField] private LayerCheck _obstacleChecker;
        [SerializeField] private Collider2D _rollCollider;

        private Vector2 _direct;
        private float _normalSpeed;

        protected override void Awake()
        {
            base.Awake();
            _normalSpeed = Creature._speed;
        }
        
        protected override IEnumerator GoToHero()
        {
            Creature.Attack();
            StartRolling();
            yield return new WaitForSeconds(_prepareTime);
            StartState(RollingForward());
        }

        private IEnumerator RollingForward()
        {
            while (!_obstacleChecker.IsTouchingLayer && _platformChecker.IsTouchingLayer)
            {
                Creature.SetDirection(_direct);
                yield return null;  
            }
            Creature._speed = _normalSpeed;
            yield return new WaitForSeconds(0.3f);
            ReturnToDefault();
            yield return new WaitForSeconds(_idleTime);
            StartState(Patrol.DoPatrol());
        }

        public void StartRollingDamage()
        {
            _rollCollider.enabled = true;
        }
        
        private void StartRolling()
        {
            _vision.gameObject.SetActive(false);
            Animator.SetBool("idle", false);
            _direct = GetDirectionToTarget();
            Creature._speed = _rollingSpeed;
        }
        
        private void ReturnToDefault()
        {
            Animator.SetBool("idle", true);
            _rollCollider.enabled = false;
            Creature.SetDirection(Vector2.zero);
            _vision.gameObject.SetActive(true);
        }

        public override void OnDie()
        {
            base.OnDie();
            _rollCollider.enabled = false;
        }
    }
}