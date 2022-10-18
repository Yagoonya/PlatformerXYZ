using System;
using PixelCrew.Components.Collectables.Potions;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/UsableItems", fileName = "UsableItems")]
    public class UsableItemsDefinition : ScriptableObject
    {
        [SerializeField] private UsableDefinition[] _items;

        public UsableDefinition Get(string id)
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
    }

    [Serializable]
    public struct UsableDefinition
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private Usable _function;

        public string Id => _id;
        public Usable Function => _function;
    }
}