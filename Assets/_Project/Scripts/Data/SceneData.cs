using Cinemachine;
using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.Data
{
    public class SceneData : MonoBehaviour
    {
        public Transform UiRoot;
        public CinemachineVirtualCamera VirtualCamera;
        public Transform DefaultPlayerSpawnPoint;
        public ResourceSourceSpawnPoint[] ResourceSourcesSpawnPoints;

        [Button]
        private void FillResourceSourcePoints()
        {
            ResourceSourcesSpawnPoints = FindObjectsOfType<ResourceSourceSpawnPoint>();
        }
    }
}