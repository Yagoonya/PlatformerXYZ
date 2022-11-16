using System;
using System.Collections.Generic;
using System.Linq;
using PixelCrew.Components.LevelManegement;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Model.Models;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        [SerializeField] private string _defaultCheckpoint;
        public PlayerData Data => _data;
        private PlayerData _save;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public QuickInventoryModel QuickInventory { get; private set; }
        public PerksModel PerksModel { get; private set; }
        public StatsModel StatsModel { get; private set; }

        private readonly List<string> _checkPoints = new List<string>();

        private void Awake()
        {
            var existSession = GetSession();
            if (existSession != null)
            {
                existSession.StartSession(_defaultCheckpoint);
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitModels();
                DontDestroyOnLoad(this);
                StartSession(_defaultCheckpoint);
            }
        }

        private void StartSession(string defaultCheckpoint)
        {
            SetChecked(defaultCheckpoint);

            LoadHud();
            SpawnHero();
        }

        private void SpawnHero()
        {
            var checkPoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckPoint = _checkPoints.Last();
            foreach (var checkPoint in checkPoints)
            {
                if (checkPoint.ID == lastCheckPoint)
                {
                    checkPoint.SpawnHero();
                    break;
                }
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(_data);
            _trash.Retain(QuickInventory);

            PerksModel = new PerksModel(_data);
            _trash.Retain(PerksModel);

            StatsModel = new StatsModel(_data);
            _trash.Retain(StatsModel);

            _data.Hp.Value = (int)StatsModel.GetValue(StatId.Hp);
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

            _trash.Dispose();
            InitModels();
        }

        private GameSession GetSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var gameSession in sessions)
            {
                if (gameSession != this)
                {
                    return gameSession;
                }
            }

            return null;
        }

        public bool IsChecked(string id)
        {
            return _checkPoints.Contains(id);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }


        public void SetChecked(string id)
        {
            if (!_checkPoints.Contains(id))
            {
                Save();
                foreach (var temp in _temporaryItems)
                {
                    _removedItems.Add(temp);
                }

                _temporaryItems.Clear();
                _checkPoints.Add(id);
            }
        }

        private List<string> _removedItems = new List<string>();
        private List<string> _temporaryItems = new List<string>();

        public void StoreState(string Id)
        {
            if (!_temporaryItems.Contains(Id))
                _temporaryItems.Add(Id);
        }

        public bool RestoreState(string Id)
        {
            return _removedItems.Contains(Id);
        }
    }
}