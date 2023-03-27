using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    [CreateAssetMenu(menuName = "Resources/FactoryConfig", fileName = "ResourceFactory")]
    public class ResourceFactoryConfig : ScriptableObject
    {
        public float ItemCreationDuration;
        public float TransfersPerSecond;
        public StackedResources NeededResources;
    }

    
    [Serializable]
    public class StackedResources : SerializableDictionaryBase<ResourceType, int>
    {
    }
}