using System.Collections.Generic;
using _Project.Scripts.MinedResources;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Path;
using UnityEngine;

namespace _Project.Scripts.Services.SaveLoad
{
    public class GameSaver : MonoBehaviour
    {
        [SerializeField] private float _saveInterval;

        private Storage _playerStorage;
        private IPathProvider _pathProvider;
        private ISaveLoadService _saveLoad;
        private float _lastSaveTime;

        public void Construct(Storage playerStorage, IPathProvider pathProvider, ISaveLoadService saveLoad)
        {
            _playerStorage = playerStorage;
            _pathProvider = pathProvider;
            _saveLoad = saveLoad;
        }

        private void Update()
        {
            if (Time.time - _lastSaveTime > _saveInterval)
                Save();
        }

#if UNITY_EDITOR
        private void OnApplicationQuit() =>
            Save();
#elif PLATFORM_ANDROID
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            Save();
    }
#endif

        [ContextMenu(nameof(Save))]
        public void Save()
        {
            var data = new PersistentData
            {
                PlayerResources = new Dictionary<ResourceType, int>(_playerStorage.Resources)
            };
            _saveLoad.Save(data, _pathProvider.GetDataPath());
            _lastSaveTime = Time.time;
        }
    }
}