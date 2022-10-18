using PixelCrew.Components.Health;
using UnityEngine;

namespace PixelCrew.Components.Collectables.Potions
{
    public class HealthPotion : Usable
    {
        [SerializeField] private int _healValue;
        
        public override void Use()
        {
            var heroHealth = GameObject.FindWithTag("Player").GetComponent<HealthComponent>();
            if (heroHealth != null)
            {
                heroHealth.ApplyChange(_healValue);
            }
        }
    }
}