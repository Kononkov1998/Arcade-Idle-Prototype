using _Project.Scripts.Data;
using _Project.Scripts.Input;
using _Project.Scripts.MinedResources;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts
{
    public class LevelEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private StaticData _staticData;

        private void Awake()
        {
            var factory = new GameFactory(_staticData);
            IInput input = factory.CreateJoystick(_sceneData.UiRoot);
            PlayerRoot player = factory.CreatePlayer(_sceneData.DefaultPlayerSpawnPoint, input);
            //factory.CreateResourceSources(_sceneData.ResourceSourcesSpawnPoints);
            factory.CreateHud(_sceneData.UiRoot, player.Inventory.Storage);
            
            _sceneData.MainCamera.Init(player.transform);
            foreach (ResourceSpawner spawner in _sceneData.ResourceSpawners) 
                spawner.Init(_staticData);
            foreach (ResourceFactory resourceFactory in FindObjectsOfType<ResourceFactory>()) 
                resourceFactory.Init(_staticData);
            foreach (FactoryView view in FindObjectsOfType<FactoryView>()) 
                view.Init(_staticData);
        }
    }
}