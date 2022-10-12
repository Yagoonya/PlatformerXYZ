using System;
using PixelCrew.Components.LevelManegement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.PauseMenu
{
    public class PauseMenuWindow : AnimatedWindow
    {
        private Action _closeAction;

        

        public void OnRestartLevel()
        {
            _closeAction = () =>
            {
                var restart = FindObjectOfType<ReloadLevelComponent>();
                restart.Reload();
            };
            Close();
        }

        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnExit()
        {
            _closeAction = () => { SceneManager.LoadScene("MainMenu"); };
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }

        public override void Close()
        {
            Time.timeScale = 1f;
            base.Close();
        }
    }
}