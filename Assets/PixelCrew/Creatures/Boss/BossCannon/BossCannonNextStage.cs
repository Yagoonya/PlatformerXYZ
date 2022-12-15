using UnityEngine;

namespace PixelCrew.Creatures.Boss.BossCannon
{
    public class BossCannonNextStage : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<SpawnerAI>();
            spawner.Stage++;

            var changeLight = animator.GetComponent<ChangeLightComponent>();
            changeLight.SetColor();
        }
    }
}