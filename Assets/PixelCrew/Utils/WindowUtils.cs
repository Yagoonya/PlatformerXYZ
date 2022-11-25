using UnityEngine;

namespace PixelCrew.Utils
{
    public static class WindowUtils
    {
        private const string PauseCanvas = "PauseCanvas";
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvases = Object.FindObjectsOfType<Canvas>();
            foreach (var canvas in canvases)
            {
                if (canvas.CompareTag(PauseCanvas))
                    Object.Instantiate(window, canvas.transform);
            }
        }
    }
}