using System;
using System.Collections.Generic;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Model.Models
{
    public class ProductModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringProperty InterfaceSelected = new StringProperty();

        public event Action OnChanged;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public ProductModel(PlayerData data)
        {
            _data = data;
            InterfaceSelected.Value = DefinitionFacade.I.Products.All[0].Id;


            _trash.Retain(InterfaceSelected.Subscribe((x, y) => OnChanged?.Invoke()));
        }
        

        public void Buy(string id, int amount)
        {
            var def = DefinitionFacade.I.Products.Get(id);
            var priceId = def.Price.ItemId;
            var priceCost = def.Price.Count * amount;

            if (!IsEnough(priceId, priceCost)) return;
            _data.Invetory.Remove(priceId, priceCost);
            _data.Invetory.Add(id, amount);

            OnChanged?.Invoke();
        }

        public bool IsEnough(string id, int amount)
        {
            var itemsInInventory = _data.Invetory.Count(id);
            return itemsInInventory > amount;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void Dispose()
        {
            _trash.Dispose();
        }
    }
}