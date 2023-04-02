using System;
using _Project.Scripts.Data;
using _Project.Scripts.Services.Factory;
using _Project.Scripts.Services.Path;
using _Project.Scripts.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Services.LevelManagement
{
    public class LevelManager
    {
        private GameFactory _gameFactory;
        private IPathProvider _pathProvider;
        private ISaveLoadService _saveLoad;
        private StaticData _staticData;
        private PersistentData _persistentData;

        public void Load(string name, Action onLoaded)
        {
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(name);
            loadingOperation.completed += _ => onLoaded?.Invoke();
        }
    }
}