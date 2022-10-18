using PixelCrew.Model.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        private PlayerData _save;
        public QuickInventoryModel QuickInventory { get; private set; }

        private void Awake()
        {
            LoadHud();
            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitModels();
                DontDestroyOnLoad(this);
            }
        }
        
        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(_data);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        public void Save()
        {
            _save = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _save.Clone();
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
