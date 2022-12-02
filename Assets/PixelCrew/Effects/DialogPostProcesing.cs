using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.Events;

namespace PixelCrew.Effects
{
    public class DialogPostProcesing : MonoBehaviour
    {
        [SerializeField] private Volume _post;
        [SerializeField] private UnityEvent _onComplete;
        private LensDistortion _lens;
        private Bloom _bloom;
        private Vignette _vignette;

        private float _time = 0.25f;

        private void Awake()
        {
            if(_post==null) return;
            _post.profile.TryGet(out _vignette);
            _post.profile.TryGet(out _lens);
            _post.profile.TryGet(out _bloom);
        }

        public void StartEffect()
        {
            StartCoroutine(LensAndBloom());
        }

        private IEnumerator EffectAnimation(float start, float end)
        {
            var time = 0f;

            while (time < _time)
            {
                time += Time.deltaTime;
                var progress = time / _time;
                var temp = Mathf.Lerp(start, end, progress);
                _lens.intensity.value = temp;
                _bloom.intensity.value = temp;
                yield return null;
            }
        }

        public void DisableVignette()
        {
            _vignette.intensity.value = 0f;
        }
        
        private IEnumerator LensAndBloom()
        {
            yield return EffectAnimation(0f, 1f);
            yield return EffectAnimation(1f, 0f);
            _onComplete?.Invoke();
            _vignette.intensity.value = 0.4f;
        }
    }
}