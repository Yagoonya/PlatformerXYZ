using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace PixelCrew.Creatures.Hero
{
    public class CandleComponent : MonoBehaviour
    {
        [SerializeField] private Cooldown _interval;
        [SerializeField] private int _fuelMaxCapacity;
        [SerializeField] private Light2D _candleLight;

        private int _currentCapacity;
        private bool _isActive = true;
        private bool _isAwayFromFuel = true;
        private float _defaultIntensity;
        private float _percentOfIntensity;

        public int CurrentCapacity => _currentCapacity;

        public void ChargeCandle(bool value)
        {
            _isAwayFromFuel = value;
            SetToMax();
            Debug.Log(value);
        }

        public void SetCandleActivity()
        {
            _isActive = !_isActive;
            gameObject.SetActive(_isActive);
        }

        private void SetToMax()
        {
            _currentCapacity = _fuelMaxCapacity;
            _candleLight.intensity = _defaultIntensity;
        }

        private void Start()
        {
            _defaultIntensity = _candleLight.intensity;
            _percentOfIntensity = _defaultIntensity / 10f;
            SetToMax();
        }

        private void Update()
        {
            if(_currentCapacity == 0) return;
            if (_isActive == false) return;
            if(_isAwayFromFuel == false) return;
            if (!_interval.IsReady) return;
            _currentCapacity--;
            _interval.Reset();
            Debug.Log($"Осталось масла: {_currentCapacity}");
            if (_currentCapacity <= 10)
            {
                _candleLight.intensity -= _percentOfIntensity;
            }
        }
    }
}