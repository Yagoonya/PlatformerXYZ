using PixelCrew.Components.GameObjectBased;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossBombing : StateMachineBehaviour
    {
        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponentInChildren<BoundsProjectileSpawner>();
            spawner.Launch();
        }
    }
}
