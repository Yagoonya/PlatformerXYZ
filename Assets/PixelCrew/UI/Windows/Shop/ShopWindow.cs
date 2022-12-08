using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository;
using PixelCrew.UI.Widget;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Shop
{
    public class ShopWindow : AnimatedWindow
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private ItemWidget _price;
        [SerializeField] private Text _name;
        [SerializeField] private Transform _shopContainer;

        private PredefinedDataGroup<ProductDef, ProductWidget> _dataGroup;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private GameSession _session;
        private int _amount;

        public int Amount => _amount;

        protected override void Start()
        {
            base.Start();

            _session = GameSession.Instance;
            _dataGroup = new PredefinedDataGroup<ProductDef, ProductWidget>(_shopContainer);

            _trash.Retain(_session.ProductModel.Subscribe(OnChanged));
            _trash.Retain(_buyButton.onClick.Subscribe(OnBuy));

            OnChanged();
        }

        private void OnBuy()
        {
            var selected = _session.ProductModel.InterfaceSelected.Value;
            _session.ProductModel.Buy(selected, _amount);
        }

        public void SetAmount(int value)
        {
            _amount = value;
        }

        public void OnChanged()
        {
            _dataGroup.SetData(DefinitionFacade.I.Products.All);

            var selected = _session.ProductModel.InterfaceSelected.Value;
            var selectedDef = DefinitionFacade.I.Products.Get(selected);
            _buyButton.interactable =
                _session.ProductModel.IsEnough(selectedDef.Price.ItemId, selectedDef.Price.Count * _amount);

            var def = DefinitionFacade.I.Products.Get(selected);
            _price.SetData(def.Price);

            _name.text = def.Name;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}