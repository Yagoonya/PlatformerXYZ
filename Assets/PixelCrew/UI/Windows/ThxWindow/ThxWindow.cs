using PixelCrew.Model;
using UnityEngine.SceneManagement;

namespace PixelCrew.UI.Windows.ThxWindow
{
    public class ThxWindow : AnimatedWindow
    {
        public void GoToMenu()
        {
            SceneManager.LoadScene("MainMenu");
            var session = GameSession.Instance;
            Destroy(session.gameObject);
            Close();
        }
    }
}