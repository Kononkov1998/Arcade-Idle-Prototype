using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    [CreateAssetMenu(menuName = "Resources/SourceConfig", fileName = "ResourceSource")]
    public class ResourceSourceConfig : ScriptableObject
    {
        public int Durability;
        public float HitsPerSecond;
        public float RecoveryDuration;
    }
}