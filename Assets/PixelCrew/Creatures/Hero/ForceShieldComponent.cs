using PixelCrew.Components.Health;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class ForceShieldComponent : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private Cooldown _cooldown;

        public void Use()
        {
            _health.Immune.Retain(this);
            _cooldown.Reset();
            gameObject.SetActive(true);
        }

        private void Update()
        {
            if(_cooldown.IsReady)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _health.Immune.Release(this);
        }
    }
}