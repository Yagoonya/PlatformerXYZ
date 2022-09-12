﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private HealthChangeEvent _onChange;

        public void ApplyChange(int changingValue)
        {
            if (_health <= 0) return;

            if (_health > 0)
            {
                _health += changingValue;
                _onChange?.Invoke(_health);
                Debug.Log($"Всего здоровья {_health} у {gameObject.name}");
            }
            if (changingValue < 0)
            {
                _onDamage?.Invoke();
            }
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        public void SetHealth(int health)
        {
            _health = health;
        }
        
        [Serializable]
        public class HealthChangeEvent: UnityEvent<int>
        {

        }
    }
}
