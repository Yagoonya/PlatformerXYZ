using UnityEngine;

namespace PixelCrew.Creatures.Boss.BossCannon
{
    public class BossSpawnSharkies : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<SpawnerAI>();
            spawner.SpawnEnemies();
        }
    }
}