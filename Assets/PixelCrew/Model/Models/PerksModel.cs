using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Utils.Disposables;

namespace PixelCrew.Model.Models
{
    public class PerksModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringProperty InterfaceSelection = new StringProperty();

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public event Action OnChanged;

        public string Used => _data.Perks.Used.Value;
        public bool IsSuperThrowSupported => _data.Perks.Used.Value == "super-throw";
        public bool IsDoubleJumpSupported => _data.Perks.Used.Value == "double-jump";
        public bool IsForceShieldSupported => _data.Perks.Used.Value == "force-shield";
        public bool IsDoppelgangerSupported => _data.Perks.Used.Value == "doppelganger";

        public PerksModel(PlayerData data)
        {
            _data = data;
            InterfaceSelection.Value = DefinitionFacade.I.Perks.All[0].Id;

            _trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void Unlock(string id)
        {
            var def = DefinitionFacade.I.Perks.Get(id);
            var isEnoughResources = _data.Invetory.IsEnough(def.Price);

            if (isEnoughResources)
            {
                _data.Invetory.Remove(def.Price.ItemId, def.Price.Count);
                _data.Perks.AddPerk(id);

                OnChanged?.Invoke();
            }
        }

        public void UsePerk(string id)
        {
            _data.Perks.Used.Value = id;
        }


        public void Dispose()
        {
            _trash.Dispose();
        }

        public bool IsUsed(string perkId)
        {
            return _data.Perks.Used.Value == perkId;
        }

        public bool IsUnlocked(string perkId)
        {
            return _data.Perks.IsUnlocked(perkId);
        }

        public bool CanBuy(string perkId)
        {
            var def = DefinitionFacade.I.Perks.Get(perkId);
            return _data.Invetory.IsEnough(def.Price);
        }
    }
}