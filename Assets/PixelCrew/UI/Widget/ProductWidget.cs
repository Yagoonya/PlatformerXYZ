using PixelCrew.Model;
using PixelCrew.Model.Definitions.Repository;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widget
{
    public class ProductWidget : MonoBehaviour, IItemRenderer<ProductDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isSelected;

        private GameSession _session;
        private ProductDef _data;
        
        private void Start()
        {
            _session = GameSession.Instance;
            UpdateView();
        }

        public void SetData(ProductDef data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _isSelected.SetActive(_session.ProductModel.InterfaceSelected.Value == _data.Id);
        }

        public void OnSelect()
        {
            _session.ProductModel.InterfaceSelected.Value = _data.Id;
        }
        
    }
}