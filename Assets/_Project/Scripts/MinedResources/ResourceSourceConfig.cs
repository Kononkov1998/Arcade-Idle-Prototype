using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    [CreateAssetMenu(menuName = "Resources/SourceConfig", fileName = "ResourceSourceConfig")]
    public class ResourceSourceConfig : ScriptableObject
    {
        public int Durability;
        public float HitsPerSecond;
        public int ResourcesCount;
        public int AmountInResourceObject;
        public float RecoveryDuration;
        public float MinResourceForce;
        public float MaxResourceForce;
        public float ResourcePickUpDelay;
    }
}