using System;
using System.Linq;
using PixelCrew.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        //[SerializeField] private LayerMask _mask;
        [SerializeField] private string[] _tags;
        [SerializeField] private OnOverlapEvent _onOverlap;
        private readonly Collider2D[] _intercationResult = new Collider2D[10];

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _radius,
                _intercationResult);

            for (var i = 0; i < size; i++)
            {
                var overlapResult = _intercationResult[i];
                var isInTags = _tags.Any(tag => _intercationResult[i].CompareTag(tag));
                if (isInTags)
                {
                    _onOverlap?.Invoke(_intercationResult[i].gameObject);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }

        [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {
        }
    }
}
