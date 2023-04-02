using _Project.Scripts.Data;
using _Project.Scripts.MinedResources.Factory;
using _Project.Scripts.MinedResources.Spawner;
using _Project.Scripts.Player;
using _Project.Scripts.Services.Factory;
using _Project.Scripts.Services.Input;
using _Project.Scripts.Services.Path;
using _Project.Scripts.Services.SaveLoad;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneData _sceneData;

        private GameFactory _gameFactory;

        private IInput _input;
        private IPathProvider _pathProvider;
        private PersistentData _persistentData;
        private PlayerRoot _player;
        private ISaveLoadService _saveLoad;
        private StaticData _staticData;

        public void Construct(GameFactory gameFactory, IPathProvider pathProvider,
            ISaveLoadService saveLoad, StaticData staticData, PersistentData persistentData)
        {
            _gameFactory = gameFactory;
            _pathProvider = pathProvider;
            _saveLoad = saveLoad;
            _staticData = staticData;
            _persistentData = persistentData;
        }

        public void Execute()
        {
            CreateGameObjects();
            InitSceneObjects();
        }

        private void CreateGameObjects()
        {
            _input = _gameFactory.CreateJoystick(_sceneData.UiRoot);
            _player =
                _gameFactory.CreatePlayer(_sceneData.DefaultPlayerSpawnPoint, _input, _persistentData.PlayerResources);
            _gameFactory.CreateHud(_sceneData.UiRoot, _player.Inventory.Storage);
        }

        private void InitSceneObjects()
        {
            _sceneData.GameSaver.Construct(_player.Inventory.Storage, _pathProvider, _saveLoad);
            _sceneData.MainCamera.Follow(_player.transform);

            foreach (ResourceSpawner spawner in _sceneData.ResourceSpawners)
                spawner.Construct(_staticData);
            foreach (ResourceFactory resourceFactory in _sceneData.ResourceFactories)
                resourceFactory.Construct();
            foreach (FactoryView view in _sceneData.FactoriesViews)
            {
                view.Construct(_staticData);
                view.Init();
            }

            if (_sceneData.Tutorial != null)
            {
                _sceneData.Tutorial.Construct(_player);
                _sceneData.Tutorial.Enable();
            }
        }
    }
}