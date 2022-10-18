using System;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
    public class InventoryItemsDefinition : ScriptableObject
    {
        [SerializeField] private ItemDefinition[] _items;

        public ItemDefinition Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if (itemDef.Id == id)
                {
                    return itemDef;
                }
            }

            return default;
        }

#if UNITY_EDITOR
        public ItemDefinition[] ItemsForEditor => _items;
#endif
    }

    [Serializable]
    public struct ItemDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTag[] _tags;
        public string Id => _id;
        public bool IsVoid => string.IsNullOrEmpty(_id);
        public Sprite Icon => _icon;

        public bool HasTag(ItemTag tag)
        {
            return _tags.Contains(tag);
        }
    }
}