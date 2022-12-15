using PixelCrew.Model.Data;
using PixelCrew.UI.Hud.Tips;
using UnityEngine;

namespace PixelCrew.Components.Tips
{
    public class ShowTipsComponent : MonoBehaviour
    {
        [SerializeField] private TipsData _data;

        private TipsBoxController _controller;

        public void Show()
        {
            if (_controller == null)
                _controller = FindObjectOfType<TipsBoxController>();
            
            _controller.ShowTips(_data);
        }
    }
}