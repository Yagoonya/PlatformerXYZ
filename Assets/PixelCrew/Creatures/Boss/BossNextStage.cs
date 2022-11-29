using PixelCrew.Components.GameObjectBased;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossNextStage : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<CircularProjectileSpawner>();
            spawner.Stage++;

            var changeLight = animator.GetComponent<ChangeLightComponent>();
            changeLight.SetColor();
        }

    }
}
