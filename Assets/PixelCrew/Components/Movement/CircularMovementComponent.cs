using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PixelCrew.Components.Movement
{
    public class CircularMovementComponent : MonoBehaviour
    {
        [Range(0, 100)] [SerializeField] private float _radius = 2f;
        [Range(0, 10)] [SerializeField] private float _speed = 0.5f;
        [SerializeField] private bool _isClockwise;

        private Transform[] _circles;
        private List<float> _angles = new List<float>();

        private float _positionX;
        private float _positionY;
        private float _ckwMultiply;

        private void OnValidate()
        {
            _ckwMultiply = _isClockwise ? -1 : 1;
            for (var i = 0; i < transform.childCount; i++)
            {
                _circles = GetComponentsInChildren<Transform>().Skip(1).ToArray();
            }
            
            var delta = CalculateAngleDeltaInRadians(_circles.Length);

            if (_circles != null)
            {
                for (int i = 0; i < _circles.Length; i++)
                {
                    var angle = i * delta;
                    _angles.Add(angle);
                    _positionX = transform.position.x + Mathf.Cos(angle) * _radius;
                    _positionY = transform.position.y + Mathf.Sin(angle) * _radius;
                    _circles[i].position = new Vector2(_positionX, _positionY);
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < _circles.Length; i++)
            {
                if (_circles[i] != null)
                {
                    _positionX = transform.position.x + Mathf.Cos(_angles[i]) * _radius;
                    _positionY = transform.position.y + Mathf.Sin(_angles[i]) * _radius * _ckwMultiply;
                    _circles[i].position = new Vector2(_positionX, _positionY);
                    _angles[i] += Time.deltaTime * _speed;
                }
            }
        }

        private float CalculateAngleDeltaInRadians(int quantity)
        {
            var angle = 360f / quantity;
            var angelInRadians = angle * (Mathf.PI / 180);
            return angelInRadians;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
        
    }
    
    
    
}