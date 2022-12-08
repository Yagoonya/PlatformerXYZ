using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.UI.Widget;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
        [SerializeField] private ActivePerkWidget _activePerk;

        private GameSession _session;
        private CompositeDisposable _trash = new CompositeDisposable();
        
        private void Start()
        {
            _session = GameSession.Instance;
            _trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHealthChanged));
            _trash.Retain(_session.PerksModel.Subscribe(OnPerkChanged));
            
            OnPerkChanged();
        }

        private void OnPerkChanged()
        {
            var activePerkId = _session.PerksModel.Used;
            var hasPerk = !string.IsNullOrEmpty(activePerkId);
            if (hasPerk)
            {
                var perkDef = DefinitionFacade.I.Perks.Get(activePerkId);
                _activePerk.Set(perkDef);
            }
            
            _activePerk.gameObject.SetActive(hasPerk);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
            var value = (float)newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}