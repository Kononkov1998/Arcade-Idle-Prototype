using System;
using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    [CreateAssetMenu(menuName = "Resources/ResourceConfig", fileName = "ResourceConfig")]
    public class ResourceConfig : ScriptableObject
    {
        public ResourceType Type;
        public Sprite Icon;
        public Sprite WhiteIcon;
        public ResourcePackedPrefab[] PackedPrefabs;

        public Resource GetResourcePrefab(int amount)
        {
            foreach (ResourcePackedPrefab packedPrefab in PackedPrefabs)
                if (packedPrefab.MinAmount <= amount && amount < packedPrefab.MaxAmountExcluded)
                    return packedPrefab.Prefab;

            return null;
        }
    }

    [Serializable]
    public struct ResourcePackedPrefab
    {
        public Resource Prefab;
        public int MinAmount;
        public int MaxAmountExcluded;
    }
}