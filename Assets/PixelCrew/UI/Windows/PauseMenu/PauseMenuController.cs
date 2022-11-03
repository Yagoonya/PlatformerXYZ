using UnityEngine;

namespace PixelCrew.UI.Windows.PauseMenu
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private Windows.PauseMenu.PauseMenu _pauseMenu;

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