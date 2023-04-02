using _Project.Scripts.Camera;
using _Project.Scripts.MinedResources;
using _Project.Scripts.MinedResources.Spawner;
using _Project.Scripts.Services.SaveLoad;
using _Project.Scripts.Tutorial;
using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.Data
{
    public class SceneData : MonoBehaviour
    {
        public Transform UiRoot;
        public MainCamera MainCamera;
        public Transform DefaultPlayerSpawnPoint;
        public ResourceSpawner[] ResourceSpawners;
        public GameSaver GameSaver;
        public StartTutorial Tutorial;
        
        [Button]
        private void CollectAll()
        {
            CollectResourceSpawners();
            MainCamera = FindObjectOfType<MainCamera>();
            GameSaver = FindObjectOfType<GameSaver>();
        }
        
        [Button]
        private void CollectResourceSpawners()
        {
            ResourceSpawners = FindObjectsOfType<ResourceSpawner>();
        }
    }
}