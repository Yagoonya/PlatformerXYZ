using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossFloodState : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponentInChildren<FloodController>();
            spawner.StartFlooding();
        }
    }
}