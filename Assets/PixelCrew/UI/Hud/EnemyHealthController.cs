using PixelCrew.Components.Health;
using PixelCrew.UI.Widget;
using UnityEngine;

namespace PixelCrew.UI.Hud
{
    public class EnemyHealthController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
        [SerializeField] private HealthComponent _health;

        private int _maxHealth;

        private void OnEnable()
        {
            _maxHealth = _health.MaxHealth;
        }

        public void OnHealthChanged()
        {
            var value = (float)_health.Health / _maxHealth;
            _healthBar.SetProgress(value);
        }
    }
}