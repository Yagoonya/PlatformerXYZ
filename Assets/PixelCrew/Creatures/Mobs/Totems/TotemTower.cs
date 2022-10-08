using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Components.Health;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Totems
{
    public class TotemTower : MonoBehaviour
    {
        [SerializeField] private List<ShootingTrapAI> _traps;
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private bool _inTurn;

        private int _currentTrap;

        private void Start()
        {
            foreach (var trap in _traps)
            {
                trap.enabled = false;
                var hp = trap.GetComponent<HealthComponent>();
                hp._onDie.AddListener(() => OnTrapDead(trap));
            }
        }

        private void OnTrapDead(ShootingTrapAI shootingTrapAI)
        {
            var index = _traps.IndexOf(shootingTrapAI);
            _traps.Remove(shootingTrapAI);
            if (index < _currentTrap)
            {
                _currentTrap--;
            }
        }

        private void Update()
        {
            if (_traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            var hasAnyTarget = _traps.Any(x => x._vision.IsTouchingLayer);
            if (hasAnyTarget)
            {
                if (_cooldown.IsReady)
                {
                    _traps[_currentTrap].RangeAttack();
                    _cooldown.Reset();
                    if (_inTurn)
                    {
                        _currentTrap = (int)Mathf.Repeat(_currentTrap + 1, _traps.Count);
                    }
                    else
                    {
                        foreach (var trap in _traps)
                        {
                            trap.RangeAttack();
                        }
                    }
                }
            }
        }
    }
}