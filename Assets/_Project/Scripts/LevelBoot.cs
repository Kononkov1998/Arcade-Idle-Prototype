using _Project.Scripts.Data;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts
{
    public class LevelBoot : MonoBehaviour
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private StaticData _staticData;

        private void Awake()
        {
            var factory = new Factory(_staticData);
            Joystick joystick = factory.CreateInputCanvas(_sceneData.UiRoot);
            PlayerRoot player = factory.CreatePlayer(_sceneData.DefaultPlayerSpawnPoint, joystick);
            //factory.CreateResourceSources(_sceneData.ResourceSourcesSpawnPoints);
            factory.CreateHud(_sceneData.UiRoot, player.Inventory);
            
            _sceneData.VirtualCamera.Follow = player.transform;
        }
    }
}