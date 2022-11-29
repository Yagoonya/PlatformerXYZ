using System;
using System.Collections;
using PixelCrew.Creatures.Weapons;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [SerializeField] private CircularProjectileSettings[] _settings;
        public int Stage { get; set; }

        [ContextMenu("Launch!")]
        public void LaunchProjectiles()
        {
            StartCoroutine(SpawnProjectiles());
        }

        private IEnumerator SpawnProjectiles()
        {
            var setting = _settings[Stage];
            var sectorStep = 2 * Mathf.PI / setting.BurstCount;
            for (int i = 0; i < setting.BurstCount; i++)
            {
                var angle = sectorStep * i;
                var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                var instance = SpawnUtils.Spawn(setting.Projectile.gameObject, transform.position);
                var projectile = instance.GetComponent<DirectionalProjectile>();
                projectile.Launch(direction);
                
                yield return new WaitForSeconds(setting.Delay);
            }
        }
    }
}

[Serializable]
public struct CircularProjectileSettings
{
    public DirectionalProjectile Projectile => _projectile;

    public int BurstCount => _burstCount;

    public float Delay => _delay;

    [SerializeField] private DirectionalProjectile _projectile;
    [SerializeField] private int _burstCount;
    [SerializeField] private float _delay;
}