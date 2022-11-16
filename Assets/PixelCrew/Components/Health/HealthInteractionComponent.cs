using System;
using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class HealthInteractionComponent : MonoBehaviour
    {
        [SerializeField] private int _changingValue;
        [SerializeField] private bool _canCrit;

        private GameSession _session;

        private void Start()
        {
            if (!_canCrit) return;
            _session = FindObjectOfType<GameSession>();
            ApplyStatsDamage();
            CalculateProbability();
        }

        private void CalculateProbability()
        {
            var probability = _session.StatsModel.GetValue(StatId.CriticalChance)/100;
            var random = UnityEngine.Random.value;
            if (random <= probability)
            {
                _changingValue *= 2;
            }
        }

        private void ApplyStatsDamage()
        {
            var statDamage = -(int)_session.StatsModel.GetValue(StatId.RangeDamage);
            _changingValue = statDamage;
        }

        public void ApplyChange(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                if (!healthComponent.isImmune)
                {
                    healthComponent.ApplyChange(_changingValue);
                    Debug.Log($"Нанесено урона: {-_changingValue}");
                }
            }
        }
    }
}