using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    [CreateAssetMenu(menuName = "Resources/SpawnerConfig", fileName = "ResourceSpawner")]
    public class ResourceSpawnerConfig : ScriptableObject
    {
        public int ResourcesCount;
        public int AmountInResourceObject;
        public float MinResourceForce;
        public float MaxResourceForce;
        public float ResourcePickUpDelay;
    }
}