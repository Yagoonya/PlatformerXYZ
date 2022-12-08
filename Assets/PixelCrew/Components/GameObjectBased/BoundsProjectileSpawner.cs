using System.Collections;
using PixelCrew.Utils.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PixelCrew.Components.GameObjectBased
{
    public class BoundsProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _burstCount;
        [SerializeField] private float _delay;
        [SerializeField] private float _spawnDelta;

        private Coroutine _coroutine;

        [ContextMenu("Launch!")]
        public void Launch()
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(SpawnProjectiles());
        }

        private IEnumerator SpawnProjectiles()
        {
            for (var i = 0; i < _burstCount; i++)
            {
                var randomPosX = Random.Range(-_spawnDelta, _spawnDelta);
                var position = transform.position;
                var spawnPos = new Vector2(position.x + randomPosX, position.y);
                Pool.Instance.Get(_prefab, spawnPos);

                yield return new WaitForSeconds(_delay);
            }

            _coroutine = null;
        }
    }
}