using System.Linq;
using _Project.Scripts.Data;
using _Project.Scripts.Services.Factory;
using _Project.Scripts.Services.LevelManagement;
using _Project.Scripts.Services.Path;
using _Project.Scripts.Services.SaveLoad;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private StaticData _staticData;
        private GameFactory _gameFactory;
        private LevelManager _levelManager;
        private IPathProvider _pathProvider;
        private PersistentData _persistentData;
        private ISaveLoadService _saveLoad;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (DestroySelfIfAnotherEntryPointExists())
                return;

            Execute();
        }

        private void Execute()
        {
            Application.targetFrameRate = 60;
            CreateServices();
            LoadData();
            LoadLevel();
        }

        private void CreateServices()
        {
#if UNITY_EDITOR
            _pathProvider = new PathProvider(_staticData);
#elif PLATFORM_ANDROID
            _pathProvider = new PersistentPathProvider(_staticData);
#endif
            _saveLoad = new JsonSaveLoadService();
            _gameFactory = new GameFactory(_staticData);
            _levelManager = new LevelManager();
        }

        private void LoadData() =>
            _persistentData = _saveLoad.Load(_pathProvider.GetDataPath());

        private void LoadLevel() =>
            _levelManager.Load(_persistentData.CurrentLevelName, InitLevel);

        private void InitLevel()
        {
            var levelEntryPoint = FindObjectOfType<LevelEntryPoint>();
            levelEntryPoint.Construct(_gameFactory, _pathProvider, _saveLoad, _staticData, _persistentData);
            levelEntryPoint.Execute();
        }

        private bool DestroySelfIfAnotherEntryPointExists()
        {
            EntryPoint[] existingEntryPoints = FindObjectsOfType<EntryPoint>();
            if (existingEntryPoints.Any(entryPoint => entryPoint != this))
            {
                Destroy(gameObject);
                return true;
            }

            return false;
        }
    }
}