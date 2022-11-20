using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        private GameObject _current;

        public GameObject Current => _current;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = SpawnUtils.Spawn(_prefab, _target.position);
            _current = instantiate;
            var scale = _target.lossyScale;
            instantiate.transform.localScale = scale;
            instantiate.SetActive(true);
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
        }
    }
}
