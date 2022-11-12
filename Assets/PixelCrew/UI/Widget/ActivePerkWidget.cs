using System;
using PixelCrew.Creatures.Hero;
using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repository;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widget
{
    public class ActivePerkWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _isCooldown;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;
        private PerkDef _data;
        private Cooldown _perkCooldown;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.PerksModel.Subscribe(OnPerkChanged));
            var hero = FindObjectOfType<Hero>();
            _perkCooldown = hero.PerkCooldown;
        }

        private void Update()
        {
            _isCooldown.fillAmount = (_perkCooldown.TimeLasts)/_perkCooldown.Value;
        }

        private void OnPerkChanged()
        {
            var usedPerk = _session.PerksModel.Used;
            var allPerks = DefinitionFacade.I.Perks.All;
            foreach (var perk in allPerks)
            {
                if (perk.Id == usedPerk)
                {
                    _data = perk;
                    _icon.sprite = _data.Icon;
                }
            }
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}