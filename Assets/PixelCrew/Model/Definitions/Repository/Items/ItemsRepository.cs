﻿using System;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Repository.Items
{
    [CreateAssetMenu(menuName = "Defs/Items", fileName = "Items")]
    public class ItemsRepository : DefRepository<ItemDefinition>
    {

#if UNITY_EDITOR
        public ItemDefinition[] ItemsForEditor => _collection;
#endif
    }

    [Serializable]
    public struct ItemDefinition : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemTag[] _tags;
        public string Id => _id;
        public bool IsVoid => string.IsNullOrEmpty(_id);
        public Sprite Icon => _icon;

        public bool HasTag(ItemTag tag)
        {
            return _tags?.Contains(tag) ?? false;
        }
    }
}