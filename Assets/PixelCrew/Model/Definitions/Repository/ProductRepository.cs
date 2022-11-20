using System;
using PixelCrew.Model.Definitions.Repository.Items;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Repository
{
    [CreateAssetMenu(menuName = "Defs/Repository/Products", fileName = "Products")]
    public class ProductRepository : DefRepository<ProductDef>
    {
    }

    [Serializable]
    public struct ProductDef : IHaveId
    {
        [InventoryId][SerializeField] private string _id;
        [SerializeField] private ItemWithCount _price;

        private ItemDefinition _item => DefinitionFacade.I.Items.Get(_id);
         
        public string Id => _id;
        public Sprite Icon => _item.Icon;
        public string Name => _item.Id;
        public ItemWithCount Price => _price;
    }
}