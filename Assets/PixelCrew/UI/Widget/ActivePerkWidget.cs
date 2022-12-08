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

        private void Start()
        {
            _session = GameSession.Instance;
        }

        private void Update()
        {
            var cooldown = _session.PerksModel.Cooldown;
            _isCooldown.fillAmount = cooldown.RemainingTime / cooldown.Value;
        }
        
        public void Set(PerkDef perkDef)
        {
            _icon.sprite = perkDef.Icon;
        }
    }
}