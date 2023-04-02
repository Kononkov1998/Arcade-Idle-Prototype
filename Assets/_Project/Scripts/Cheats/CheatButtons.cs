using System.Collections.Generic;
using _Project.Scripts.MinedResources;
using _Project.Scripts.Player;
using NaughtyAttributes;
using UnityEngine;

namespace _Project.Scripts.Cheats
{
    public class CheatButtons : MonoBehaviour
    {
        [Button]
        private void GetResources()
        {
            FindObjectOfType<PlayerRoot>().Inventory.Storage.AddResources(new Dictionary<ResourceType, int>
            {
                {ResourceType.Crystal, 1000},
                {ResourceType.Metal, 1000},
                {ResourceType.Wood, 1000}
            });
        }
    }
}