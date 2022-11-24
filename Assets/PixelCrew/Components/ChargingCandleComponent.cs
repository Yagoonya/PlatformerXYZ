using PixelCrew.Creatures.Hero;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ChargingCandleComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !col.gameObject.CompareTag(_tag)) return;
            var candle = col.GetComponentInChildren<CandleComponent>();
            if (candle != null)
                candle.ChargeCandle(false);
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.gameObject.IsInLayer(_layer)) return;
            if (!string.IsNullOrEmpty(_tag) && !col.gameObject.CompareTag(_tag)) return;
            var candle = col.GetComponentInChildren<CandleComponent>();
            if (candle != null)
                candle.ChargeCandle(true);
        }
    }
}