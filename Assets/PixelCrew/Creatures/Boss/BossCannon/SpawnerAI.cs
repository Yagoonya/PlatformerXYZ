using System.Collections;
using PixelCrew.Components.GameObjectBased;
using PixelCrew.Creatures.Boss.Bombs;
using PixelCrew.Creatures.Mobs;
using UnityEngine;

namespace PixelCrew.Creatures.Boss.BossCannon
{
    public class SpawnerAI : MonoBehaviour
    {
        [SerializeField] private SpawnComponent[] _spawners;
        private Coroutine _coroutine;

        public int Stage = 1;

        public void SpawnEnemies()
        {
            if(_coroutine != null)
                StopCoroutine(_coroutine);
            var sharkies = FindObjectsOfType<MobAI>();
            if(sharkies.Length >= Stage * 2) return;
            _coroutine = StartCoroutine(StartSpawn());
        }

        private IEnumerator StartSpawn()
        {
            for (int i = 0; i < Stage; i++)
            {
                foreach (var spawner in _spawners)
                {
                    spawner.Spawn();
                }

                yield return new WaitForSeconds(0.5f);
            }
            _coroutine = null;
        }

        public void ClearBossArena()
        {
            var sharkies = FindObjectsOfType<MobAI>();
            var bombs = FindObjectsOfType<Bomb>();

            foreach (var sharky in sharkies)
            {
                Destroy(sharky.gameObject);
            }
            
            foreach (var bomb in bombs)
            {
                Destroy(bomb.gameObject);
            }
        }
    }
}