using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/ThrowableItems", fileName = "ThrowableItems")]
    public class ThrowableItemsDefinition : ScriptableObject
    {
        [SerializeField] private ThrowableDefinition[] _items;

        public ThrowableDefinition Get(string id)
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
    public struct ThrowableDefinition
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;
    }
}