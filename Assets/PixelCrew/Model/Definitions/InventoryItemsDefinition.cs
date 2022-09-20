using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
    public class InventoryItemsDefinition : ScriptableObject
    {
        [SerializeField] private ItemDefenition[] _items;

        public ItemDefenition Get(string id)
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
        public ItemDefenition[] ItemsForEditor => _items;
#endif
    }

    [Serializable]
    public struct ItemDefenition
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isStackable;
        public string Id => _id;
        public bool IsStackable => _isStackable;
        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
}