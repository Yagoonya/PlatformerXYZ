using PixelCrew.Model;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ChargingCandleComponent : MonoBehaviour
    {
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        public void Refill()
        {
            _session.Data.Fuel.Value = 100;
        }
    }
}