using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository;
using PixelCrew.UI.Windows.Shop;
using UnityEngine;

namespace PixelCrew.UI.Widget
{
    public class ShopItemWidget : ItemWidget
    {
        [SerializeField] private ShopWindow _shop;
        
        public override void SetData(ItemWithCount price)
        {
            var def = DefinitionFacade.I.Items.Get(price.ItemId);
            _icon.sprite = def.Icon;
            var finalCost = price.Count * _shop.Amount;
            _value.text = finalCost.ToString();
        }
    }
}