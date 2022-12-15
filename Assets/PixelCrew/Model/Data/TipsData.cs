using System;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class TipsData
    {
        [SerializeField] private Sprite _iconLeft;
        [SerializeField] private Sprite _iconRight;
        [SerializeField] private string tipsText;

        public Sprite IconLeft => _iconLeft;

        public Sprite IconRight => _iconRight;

        public string TipsText => tipsText;
    }
}