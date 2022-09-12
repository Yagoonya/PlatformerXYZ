using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Totems
{
    public class TotemHolder : MonoBehaviour
    {
        [SerializeField] private Cooldown _cooldown;

        private TotemsAI[] _totems;
        private int _currentIndex;
        private bool _isActive = false;

        private void OnValidate()
        {
            for (var i = 0; i <= transform.childCount; i++)
            {
                _totems = GetComponentsInChildren<TotemsAI>();
            }
                        
            if (_totems != null)
            {
                for (int i = 0; i < _totems.Length; i++)
                {
                    _totems[i].transform.position = new Vector3(transform.position.x, transform.position.y + i / 1.4f,
                        transform.position.z);
                }
            }
        }

        private void Update()
        {
            if (_isActive)
            {
                if (_cooldown.IsReady)
                {
                    StartShooting();
                }
            }
        }

        private void StartShooting()
        {
            if (_totems[_currentIndex] != null)
            {
                _cooldown.Reset();
                _totems[_currentIndex].Activate();
            }

            _currentIndex++;
            if (_currentIndex >= _totems.Length)
                _currentIndex = 0;
        }

        public void Activate()
        {
            _isActive = true;
        }
    }
}