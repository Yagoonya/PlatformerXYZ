using System;
using System.Collections;
using PixelCrew.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PixelCrew.Components.GameObjectBased
{
    public class BoundsProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private int _burstCount;
        [SerializeField] private float _delay;
        [SerializeField] private float _spawnDelta;
        
        [ContextMenu("Launch!")]
        public void Launch()
        {
            StartCoroutine(SpawnProjectiles());
        }

        private IEnumerator SpawnProjectiles()
        {
            for (int i = 0; i < _burstCount; i++)
            {
                var randomPosX = Random.Range(-_spawnDelta, _spawnDelta);
                var position = transform.position;
                var spawnPos = new Vector2(position.x + randomPosX, position.y);
                SpawnUtils.Spawn(_projectile, spawnPos);

                yield return new WaitForSeconds(_delay);
            }
        }
    }
}