using System.Collections.Generic;
using _Project.Scripts.Camera;
using _Project.Scripts.MinedResources;
using _Project.Scripts.MinedResources.Factory;
using _Project.Scripts.MinedResources.Spawner;
using _Project.Scripts.Services.SaveLoad;
using _Project.Scripts.Tutorial;
using _Project.Scripts.UI;
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
        public ResourceFactory[] ResourceFactories;
        public FactoryView[] FactoriesViews;
        public GameSaver GameSaver;
        public StartTutorial Tutorial;

        [Button]
        private void CollectAll()
        {
            ResourceSpawners = FindObjectsOfType<ResourceSpawner>();
            ResourceFactories = FindObjectsOfType<ResourceFactory>();
            FactoriesViews = FindObjectsOfType<FactoryView>();
            MainCamera = FindObjectOfType<MainCamera>();
            GameSaver = FindObjectOfType<GameSaver>();
        }
    }
}