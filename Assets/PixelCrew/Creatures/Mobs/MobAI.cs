using System.Collections;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] protected ColliderCheck _vision;
        [SerializeField] private ColliderCheck _canAttack;

        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _missCooldown = 1f;
        private IEnumerator _current;
        private GameObject _target;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        private SpawnListComponent _particles;
        protected Creature Creature;
        protected Animator Animator;
        private bool _isDead;
        protected Patrol Patrol;

        protected virtual void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            Creature = GetComponent<Creature>();
            Animator = GetComponent<Animator>();
            Patrol = GetComponent<Patrol>();
        }
        
        
        
        private void Start()
        {
            StartState(Patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;

            _target = go;

            StartState(AgroToHero());

        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);

            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            Creature.SetDirection(Vector2.zero);
            Creature.UpdateSpriteDirection(direction);
        }

        protected virtual IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }

            _particles.Spawn("MissHero");
            Creature.SetDirection(Vector2.zero);
            yield return new WaitForSeconds(_missCooldown);

            StartState(Patrol.DoPatrol());
        }

        private IEnumerator Attack()
        {
            while(_canAttack.IsTouchingLayer)
            {
                Creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            Creature.SetDirection(direction);
        }

        protected Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        protected void StartState(IEnumerator coroutine)
        {
            Creature.SetDirection(Vector2.zero);

            if (_current != null)
                StopCoroutine(_current);

            _current = coroutine; 
            StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            Creature.SetDirection(Vector2.zero);
            _isDead = true;
            Animator.SetBool(IsDeadKey, true);

            if (_current != null)
                StopCoroutine(_current);
        }
    }
}