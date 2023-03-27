using _Project.Scripts.Camera;
using _Project.Scripts.MinedResources;
using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.Data
{
    public class SceneData : MonoBehaviour
    {
        public Transform UiRoot;
        public MainCamera MainCamera;
        public Transform DefaultPlayerSpawnPoint;
        public ResourceSourceSpawnPoint[] ResourceSourcesSpawnPoints;
        public ResourceSpawner[] ResourceSpawners;

        [Button]
        private void FillResourceSourcePoints()
        {
            ResourceSourcesSpawnPoints = FindObjectsOfType<ResourceSourceSpawnPoint>();
        }
        
        [Button]
        private void CollectResourceSpawners()
        {
            ResourceSpawners = FindObjectsOfType<ResourceSpawner>();
        }
    }
}