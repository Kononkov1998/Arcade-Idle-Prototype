using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    [CreateAssetMenu(menuName = "Resources/ResourceConfig", fileName = "ResourceConfig")]
    public class ResourceConfig : ScriptableObject
    {
        public ResourceType Type;
        public Sprite Icon;
    }
}