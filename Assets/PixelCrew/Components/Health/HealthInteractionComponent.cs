using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class HealthInteractionComponent : MonoBehaviour
    {
        [SerializeField] private int _changingValue;

        public void SetDelta(int delta)
        {
            _changingValue = delta;
        }
        
        public void ApplyChange(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyChange(_changingValue);
                Debug.Log($"Нанесено урона: {-_changingValue}");
            }
        }
    }
}