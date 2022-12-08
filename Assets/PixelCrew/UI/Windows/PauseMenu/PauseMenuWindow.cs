using System;
using PixelCrew.Components.LevelManegement;
using PixelCrew.Model;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.Windows.PauseMenu
{
    public class PauseMenuWindow : AnimatedWindow
    {
        private Action _closeAction;

        private float defaultTimeScale;

        protected override void Start()
        {
            base.Start();

            defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

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
            WindowUtils.CreateWindow("UI/SettingsWindow");
        }

        public void OnExit()
        {
            _closeAction = () =>
            {
                SceneManager.LoadScene("MainMenu");

                var session = GameSession.Instance;
                Destroy(session.gameObject);
            };
            Close();
        }

        protected override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }

        private void OnDestroy()
        {
            Time.timeScale = defaultTimeScale;
        }
    }
}