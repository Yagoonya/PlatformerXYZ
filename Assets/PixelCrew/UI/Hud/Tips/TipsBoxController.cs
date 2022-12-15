using PixelCrew.Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.Tips
{
    public class TipsBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Image _leftIcon;
        [SerializeField] private Image _rightIcon;
        [SerializeField] private Text _text;

        private TipsData _data;

        public void ShowTips(TipsData data)
        {
            _text.text = string.Empty;
            _data = data;

            _leftIcon.sprite = _data.IconLeft;
            _rightIcon.sprite = _data.IconRight;
            _text.text = _data.TipsText;
            _container.SetActive(true);
        }

        public void CloseWindow()
        {
            _text.text = string.Empty;
            _container.SetActive(false);
        }
    }
}