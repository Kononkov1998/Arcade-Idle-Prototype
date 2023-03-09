using System.Collections.Generic;
using _Project.Scripts.Data;
using _Project.Scripts.MinedResources;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class Factory
    {
        private readonly StaticData _staticData;

        public Factory(StaticData staticData) =>
            _staticData = staticData;

        public PlayerRoot CreatePlayer(Transform transform, Joystick joystick)
        {
            PlayerRoot player = Object.Instantiate(_staticData.PlayerPrefab, transform.position, Quaternion.identity);
            player.Init(joystick);
            return player;
        }

        public Joystick CreateInputCanvas(Transform parent)
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
                _staticData.GetResourceSourcePrefab(resourceSourceSpawnPoint.ResourceSourceType),
                spawnPoint.position,
                spawnPoint.rotation
            );
        }

        public void CreateHud(Transform parent, Inventory inventory)
        {
            Hud hud = Object.Instantiate(_staticData.HudPrefab, parent);
            hud.Init(inventory.Resources);
            foreach (ResourceConfig config in _staticData.ResourcesConfigs)
            {
                ResourceView view = Object.Instantiate(_staticData.ResourceViewPrefab, hud.ResourcesParent);
                view.Init(config.Type, 0, config.Icon);
                hud.AddResourceView(config.Type, view);
            }
        }
    }
}