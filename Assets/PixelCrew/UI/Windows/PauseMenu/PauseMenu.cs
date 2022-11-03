using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.UI.Windows.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        private void OnEnable()
        {
            OnShowPauseWindow();
        }

        private void OnDisable()
        {
            var window = FindObjectOfType<PauseMenuWindow>();
            if (window != null)
                window.Close();
        }

        private void OnShowPauseWindow()
        {
            WindowUtils.CreateWindow("UI/PauseMenu");
        }
    }
}