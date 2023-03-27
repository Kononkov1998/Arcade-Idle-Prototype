using System.Collections.Generic;
using _Project.Scripts.Data;
using _Project.Scripts.Input;
using _Project.Scripts.MinedResources;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class GameFactory
    {
        private readonly StaticData _staticData;

        public GameFactory(StaticData staticData) =>
            _staticData = staticData;

        public PlayerRoot CreatePlayer(Transform transform, IInput input)
        {
            PlayerRoot player = Object.Instantiate(_staticData.PlayerPrefab, transform.position, Quaternion.identity);
            player.Init(_staticData, input);
            return player;
        }

        public Joystick CreateJoystick(Transform parent)
        {
            Canvas canvas = Object.Instantiate(_staticData.JoystickCanvasPrefab, parent);
            return canvas.GetComponentInChildren<Joystick>();
        }

        public void CreateResourceSources(IEnumerable<ResourceSourceSpawnPoint> resourceSourcesSpawnPoints)
        {
            foreach (ResourceSourceSpawnPoint resourceSourceSpawnPoint in resourceSourcesSpawnPoints)
                CreateResourceSource(resourceSourceSpawnPoint);
        }

        private void CreateResourceSource(ResourceSourceSpawnPoint resourceSourceSpawnPoint)
        {
            Transform spawnPoint = resourceSourceSpawnPoint.transform;
            Object.Instantiate(
                _staticData.GetResourceSourcePrefab(resourceSourceSpawnPoint.ResourceSourceId),
                spawnPoint.position,
                spawnPoint.rotation
            );
        }

        public void CreateHud(Transform parent, Storage storage)
        {
            Hud hud = Object.Instantiate(_staticData.HudPrefab, parent);
            hud.Init(storage.Resources);
            foreach (ResourceConfig config in _staticData.ResourcesConfigs)
            {
                ResourceView view = Object.Instantiate(_staticData.ResourceViewPrefab, hud.ResourcesParent);
                int resourceAmount = storage.HasResource(config.Type) ? storage.Resources[config.Type] : 0;
                view.Init(resourceAmount, config.Icon);
                hud.AddResourceView(config.Type, view);
            }
        }
    }
}