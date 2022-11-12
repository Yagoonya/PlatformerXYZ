using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class HealthInteractionComponent : MonoBehaviour
    {
        [SerializeField] private int _changingValue;

        public void ApplyChange(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                if (!healthComponent.isImmune)
                    healthComponent.ApplyChange(_changingValue);
            }
        }
    }
}