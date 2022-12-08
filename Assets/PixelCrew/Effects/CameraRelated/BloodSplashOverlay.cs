using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.Effects.CameraRelated
{   
    [RequireComponent(typeof(Animator))]
    public class BloodSplashOverlay : MonoBehaviour
    {
        [SerializeField] private Image _overlay;

        private static readonly int Health = Animator.StringToHash("Health");

        private Animator _animator;
        private GameSession _session;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            _session = GameSession.Instance;
            _trash.Retain(_session.Data.Hp.SubscribeAndInvoke(OnHpChanged));
        }

        private void OnHpChanged(int newValue, int _)
        {
            var maxHp = _session.StatsModel.GetValue(StatId.Hp);
            var hpNormalized = newValue / maxHp; 
            _animator.SetFloat(Health, hpNormalized);
            Debug.Log($"{maxHp} / {newValue} = {hpNormalized}");
            
            var value = 1 - hpNormalized;
            _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, value);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}