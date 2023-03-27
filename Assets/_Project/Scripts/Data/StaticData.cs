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
        public ResourceSource[] ResourceSourcePrefabs;
        public ResourceConfig[] ResourcesConfigs; 

        public ResourceSource GetResourceSourcePrefab(ResourceSourceId resourceSourceId) =>
            ResourceSourcePrefabs.First(x => x.Id == resourceSourceId);
        
        public Resource GetResourcePrefab(ResourceType resourceType, int amount) =>
            ResourcesConfigs.First(x => x.Type == resourceType).GetResourcePrefab(amount);
        
        public ResourceConfig GetResourceConfig(ResourceType resourceType) =>
            ResourcesConfigs.First(x => x.Type == resourceType);
    }
}