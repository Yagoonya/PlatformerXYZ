using System.Collections;
using PixelCrew.Components.ColliderBased;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Patrolling
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private LayerCheck _platformChecker;
        [SerializeField] private LayerCheck _obstacleChecker;
        [SerializeField] private Creature _creature;
        [SerializeField] private int _direction;

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (_platformChecker.IsTouchingLayer && !_obstacleChecker.IsTouchingLayer)
                {
                    _creature.SetDirection(new Vector2(_direction, 0));
                }
                else
                {
                    _direction = -_direction;
                    _creature.SetDirection(new Vector2(_direction, 0));
                }

                yield return null;
            }
        }
    }
}