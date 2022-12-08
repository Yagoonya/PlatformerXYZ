using System.Collections;
using PixelCrew.Components.GameObjectBased;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class DoppelgangerComponent : MonoBehaviour
    {
        [SerializeField] private SpawnComponent _spawner;
        [SerializeField] private Transform _hero;
        [SerializeField] private float _duration;


        public void Spawn()
        {
            StartCoroutine(SpawnDoppelganger());
        }

        private IEnumerator SpawnDoppelganger()
        {
            _spawner.Spawn();
            var teleportPosition = _spawner.Current;
            var sprite = teleportPosition.GetComponent<SpriteRenderer>();
            yield return AlphaAnimation(sprite, 0);;
            if (teleportPosition != null)
            {
                _hero.position = teleportPosition.transform.position;
                Destroy(teleportPosition);
            }
        }
        
        private IEnumerator AlphaAnimation(SpriteRenderer sprite, float destAlpha)
        {
            var time = 0f;
            var spriteAlpha = sprite.color.a;
            while(time < _duration)
            {
                time += Time.deltaTime;
                var progress = time / _duration;
                var tmpAlpha = Mathf.Lerp(spriteAlpha, destAlpha, progress);
                var color = sprite.color;
                color.a = tmpAlpha;
                sprite.color = color;

                yield return null;
            }
        }
    }
}