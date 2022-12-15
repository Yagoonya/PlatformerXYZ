using PixelCrew.Components.GameObjectBased;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossBombing : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponentInChildren<BoundsProjectileSpawner>();
            spawner.Launch();
        }
    }
}
