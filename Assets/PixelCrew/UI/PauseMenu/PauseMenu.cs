using UnityEngine;

namespace PixelCrew.UI.PauseMenu
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
            var window = Resources.Load<GameObject>("UI/PauseMenu");
            var canvas = GameObject.FindWithTag("PauseCanvas").GetComponent<Canvas>();
            Instantiate(window, canvas.transform);
        }
    }
}