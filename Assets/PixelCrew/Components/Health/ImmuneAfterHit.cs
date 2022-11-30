﻿using System.Collections;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.Components.Health
{
    [RequireComponent(typeof(HealthComponent))]
    public class ImmuneAfterHit : MonoBehaviour
    {
        [SerializeField] private float immuneDuration;
        private HealthComponent _health;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private Coroutine _coroutine;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _trash.Retain(_health._onDamage.Subscribe(OnDamage));
        }

        private void OnDamage()
        {
            TryStop();
            if (immuneDuration > 0)
                _coroutine = StartCoroutine(MakeImmune());
        }

        private void TryStop()
        {
            if(_coroutine!=null)
                StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator MakeImmune()
        {
            _health.Immune.Retain(this);
            yield return new WaitForSeconds(immuneDuration);
            _health.Immune.Release(this);
        }

        private void OnDestroy()
        {
            TryStop();
            _trash.Dispose();
        }
    }
}