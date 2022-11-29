using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace PixelCrew.Creatures.Boss
{
    public class ChangeLightComponent : MonoBehaviour
    {
        [SerializeField] private Light2D[] _lights;
        private Color[] _colors;

        [ColorUsage(true, true)] [SerializeField]
        private Color _color;
        
        [ContextMenu("Set")]
        public void SetColor()
        {
            foreach (var light in _lights)
            {
                light.color = _color;
            }
        }
        
    }
}