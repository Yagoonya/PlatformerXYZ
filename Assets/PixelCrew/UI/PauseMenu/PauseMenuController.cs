using UnityEngine;

namespace PixelCrew.UI.PauseMenu
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private PauseMenu _pauseMenu;

        private bool _isActive = false;
        
        [ContextMenu("PauseGame")]
        public void PauseGame()
        {
            var state = !_isActive;
            _pauseMenu.gameObject.SetActive(state);
            _isActive = !_isActive;
        }

    }
}