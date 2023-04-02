using System.Linq;
using _Project.Scripts.MinedResources;
using _Project.Scripts.Player;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(menuName = "StaticData", fileName = "StaticData")]
    public class StaticData : ScriptableObject
    {
        public Canvas JoystickCanvasPrefab;
        public Hud HudPrefab;
        public ResourceView ResourceViewPrefab;
        public ResourceView NeededResourceViewPrefab;
        public PlayerRoot PlayerPrefab;
        public ResourceConfig[] ResourcesConfigs;
        public string SaveFileName;

        public Resource GetResourcePrefab(ResourceType resourceType, int amount) =>
            ResourcesConfigs.First(x => x.Type == resourceType).GetResourcePrefab(amount);

        public ResourceConfig GetResourceConfig(ResourceType resourceType) =>
            ResourcesConfigs.First(x => x.Type == resourceType);
    }
}