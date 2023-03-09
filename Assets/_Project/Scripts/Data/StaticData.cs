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
        public PlayerRoot PlayerPrefab;
        public ResourceSource[] ResourceSourcePrefabs;
        public ResourceConfig[] ResourcesConfigs; 

        public ResourceSource GetResourceSourcePrefab(ResourceSourceType resourceSourceType) =>
            ResourceSourcePrefabs.First(x => x.Type == resourceSourceType);
    }
}