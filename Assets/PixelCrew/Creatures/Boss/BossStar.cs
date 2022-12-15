using PixelCrew.Creatures.Boss.Bombs;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossStar : MonoBehaviour
    {
        public void ClearBossArena()
        {
            var bombs = FindObjectsOfType<Bomb>();

            foreach (var bomb in bombs)
            {
                Destroy(bomb.gameObject);
            }
        }
    }
}