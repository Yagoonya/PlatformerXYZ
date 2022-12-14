using UnityEngine;

namespace PixelCrew.Components.Movement
{
    public class CircularMovementComponent : MonoBehaviour
    {
        [Range(0, 100)] [SerializeField] private float _radius = 2f;
        [Range(0, 10)] [SerializeField] private float _speed = 0.5f;
        [SerializeField] private bool _isClockwise;

        private Rigidbody2D[] _bodies;
        private Vector2[] _positions;
        private float _time;

        private void Awake()
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            _bodies = GetComponentsInChildren<Rigidbody2D>();
            _positions = new Vector2[_bodies.Length];
        }

        private void Update()
        {
            CalculatePositions();
            var isAllDead = true;
            for (var i = 0; i < _bodies.Length; i++)
            {
                if (_bodies[i])
                {
                    _bodies[i].MovePosition(_positions[i]);
                    isAllDead = false;
                }
            }

            if (isAllDead)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            _time += Time.deltaTime;
        }

        private void CalculatePositions()
        {
            var multiplier = _isClockwise ? -1 : 1;
            var step = 2 * Mathf.PI / _bodies.Length;

            Vector2 containerPosition = transform.position;
            for (var i = 0; i < _bodies.Length; i++)
            {
                var angle = step * i;
                var pos = new Vector2(
                    Mathf.Cos(angle + _time * _speed ) * _radius,
                    Mathf.Sin(angle + _time * _speed) * _radius * multiplier
                );
                _positions[i] = containerPosition + pos;
            }
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateContent();
            CalculatePositions();
            for (var i = 0; i < _bodies.Length; i++)
            {
                _bodies[i].transform.position = _positions[i];
            }
        }

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, _radius);
        }
#endif
    }
   
}