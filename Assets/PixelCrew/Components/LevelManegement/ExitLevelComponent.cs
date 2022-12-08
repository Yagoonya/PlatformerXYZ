using PixelCrew.Model;
using PixelCrew.UI.LevelLoader;
using UnityEngine;

namespace PixelCrew.Components.LevelManegement
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            var session = GameSession.Instance;
            session.Save();
            var loader = FindObjectOfType<LevelsLoader>();
            loader.LoadLevel(_sceneName);
        }
    }
}
