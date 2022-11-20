using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widget
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] protected Image _icon;
        [SerializeField] protected Text _value;

        public virtual void SetData(ItemWithCount price)
        {
            var def = DefinitionFacade.I.Items.Get(price.ItemId);
            _icon.sprite = def.Icon;

            _value.text = price.Count.ToString();
        }
    }
}