using PixelCrew.Creatures.Hero;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.Collectables.Potions
{
    public class SpeedPotion : Usable
    {
        [SerializeField] private int _speedDelta;

        private Hero _hero;

        private float _defaultSpeed;
        
        public override void Use()
        {
            _hero = GameObject.FindWithTag("Player").GetComponent<Hero>();
            if (_hero != null)
            {
                _defaultSpeed = _hero._speed;
                _hero._speed += _speedDelta;
                Invoke(nameof(ReturnToDefault), 2);
            }
        }

        private void ReturnToDefault()
        {
            _hero._speed = _defaultSpeed;
        }
    }
}