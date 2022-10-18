﻿using System.Collections;
using System.Linq;
using PixelCrew.Components;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Utils;
using UnityEditor.Animations;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class Hero : Creature, ICanAddInInventory
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private ColliderCheck _wallCheck;
        [SerializeField] private float _fallDownVelocity;

        [Space] [Header("ThrowParameters")]
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private Cooldown _attackCooldown;
        [SerializeField] private int _numberOfThrows = 3;
        [SerializeField] private float _timeBetweenThrows = 0.2f;
        [SerializeField] private SpawnComponent _throwSpawner;
        
        [Space] [Header("Animators")]
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Space] [Header("DropsAfterHit")] 
        [SerializeField] private ProbabilityDropComponent _hitDrop;

        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsClimbing = Animator.StringToHash("is-climbing");

        private bool _allowDoubleJump;
        private bool _isOnWall;

        private GameSession _session;
        private float _defaultGravityScale;

        private const string SwordId = "Sword";

        private int CoinsCount => _session.Data.Invetory.Count("Coin");
        private int SwordCount => _session.Data.Invetory.Count(SwordId);
        private int HealthPotionCount => _session.Data.Invetory.Count("HealPotion");

        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId)
                    return SwordCount > 1;
                
                var def = DefinitionFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        private bool CanUse
        {
            get
            {
                if (SelectedItemId == "HealPotion" || SelectedItemId == "BigHealthPotion" || SelectedItemId == "SpeedPotion")
                {
                    var def = DefinitionFacade.I.Items.Get(SelectedItemId);
                    return def.HasTag(ItemTag.Usable);
                }

                return false;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = RigidBody.gravityScale;
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.Hp.Value = currentHealth;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();
            _session.Data.Invetory.OnChanged += OnInventoryChanged;

            health.SetHealth(_session.Data.Hp.Value);
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Invetory.OnChanged -= OnInventoryChanged;
        }
        
        private void OnInventoryChanged(string id, int value)
        {
            if(id == SwordId)
                UpdateHeroWeapon();
        }

        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;
            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
            {
                _isOnWall = true;
                RigidBody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                RigidBody.gravityScale = _defaultGravityScale;
            }

            Animator.SetBool(IsClimbing, _isOnWall);
        }

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;

            if (IsGrounded || _isOnWall)
            {
                _allowDoubleJump = true;
            }

            if (!isJumpPressing && _isOnWall)
            {
                return 0f;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            if (!IsGrounded && _allowDoubleJump && !_isOnWall)
            {
                _allowDoubleJump = false;
                DoJumpVfx();
                return _jumpForce;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Invetory.Add(id, value);
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.IsInLayer(_groundLayer))
            {
                var contact = collision.contacts[0];
                if (contact.relativeVelocity.y >= _fallDownVelocity)
                {
                    _particles.Spawn("SlamDown");
                }
            }
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinsCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
            _session.Data.Invetory.Remove("Coin", numCoinsToDispose);
            
            _hitDrop.SetCount(numCoinsToDispose);
            _hitDrop.CalculateDrop();
        }

        public override void Attack()
        {
            if (SwordCount <= 0) return;
            if (_attackCooldown.IsReady)
            {
                base.Attack();
                _attackCooldown.Reset();
            }
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordCount > 0 ? _armed : _disarmed;
        }

        public void OnThrowing()
        {
            Sounds.Play("Range");

            var throwableId = _session.QuickInventory.SelectedItem.Id;
            var throwableDefinition = DefinitionFacade.I.Throwable.Get(throwableId);
            _throwSpawner.SetPrefab(throwableDefinition.Projectile);
            _throwSpawner.Spawn();
            
            _session.Data.Invetory.Remove(throwableId, 1);
        }

        public void Throw()
        {
            if (_throwCooldown.IsReady && CanThrow)
            {
                Animator.SetTrigger(ThrowKey);
                _throwCooldown.Reset();
            }
            else
            {
                Debug.Log("Остался последный меч");
            }
        }

        public void UsePotion()
        {
            if (CanUse)
            {
                var usableId = _session.QuickInventory.SelectedItem.Id;
                var usableDefinition = DefinitionFacade.I.Usable.Get(usableId);
                Sounds.Play("Heal");
                _particles.Spawn("Heal");
                usableDefinition.Function.Use();
                Debug.Log($"Used{usableId}");
                _session.Data.Invetory.Remove(usableId,1);
            }
            else
            {
                Debug.Log("Зелье не выбрано!");
            }
        }

        public void HoldingThrow()
        {
            StartCoroutine(MultiThrowing(_numberOfThrows, _timeBetweenThrows));
        }

        private IEnumerator MultiThrowing(int amount, float cooldown)
        {
            for (var i = 0; i < amount; i++)
            {
                Throw();
                yield return new WaitForSeconds(cooldown);
            }
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }
    }
}