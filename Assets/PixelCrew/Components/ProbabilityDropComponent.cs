﻿using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

namespace PixelCrew.Components
{
    public class ProbabilityDropComponent : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private DropDate[] _drop;
        [SerializeField] private DropEvent _onDropCalculated;
        [SerializeField] private bool _spawnOnEnable;

        private void OnEnable()
        {
            if (_spawnOnEnable)
            {
                CalculateDrop();
            }
        }

        [ContextMenu("CalculateDrop")]
        public void CalculateDrop()
        {
            var itemsToDrop = new GameObject[_count];
            var itemCount = 0;
            var total = _drop.Sum(dropData => dropData.Probability);
            var sortedDrop = _drop.OrderBy(dropData => dropData.Probability);

            while (itemCount < _count)
            {
                var random = UnityEngine.Random.value * total;
                var current = 0f;
                foreach (var dropData in sortedDrop)
                {
                    current += dropData.Probability;
                    if (current >= random)
                    {
                        itemsToDrop[itemCount] = dropData.Drop;
                        itemCount++;
                        break;
                    }
                }
            }

            _onDropCalculated?.Invoke(itemsToDrop);
        }

        public void SetCount(int count)
        {
            _count = count;
        }

        [Serializable]
        public class DropDate
        {
            public GameObject Drop;
            [Range(0f, 100f)] public float Probability;
        }

        [Serializable]
        public class DropEvent : UnityEvent<GameObject[]>
        {
        }
    }
}