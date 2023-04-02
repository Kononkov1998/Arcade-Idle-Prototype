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
        private IPathProvider _pathProvider;
        private ISaveLoadService _saveLoad;
        private GameFactory _gameFactory;
        private PersistentData _persistentData;
        private LevelManager _levelManager;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (DestroySelfIfAnotherEntryPointExists())
                return;
            Execute();
        }

        private void Execute()
        {
            CreateServices();
            LoadData();
            LoadLevel();
        }

        private void CreateServices()
        {
#if UNITY_EDITOR
            _pathProvider = new PathProvider(_staticData);
#elif PLATFORM_ANDROID
            _pathProvider = new PersistentPathProvider();
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
            levelEntryPoint.Init();
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