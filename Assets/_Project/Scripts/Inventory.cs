using System;
using System.Collections.Generic;
using _Project.Scripts.MinedResources;
using UniRx;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class Inventory
    {
        private ReactiveDictionary<ResourceType, int> _resources;
        public IReadOnlyReactiveDictionary<ResourceType, int> Resources => _resources;

        public Inventory()
        {
            _resources = new ReactiveDictionary<ResourceType, int>();
        }

        public void AddResource(ResourceType type, int amount)
        {
            if (amount < 1)
                Debug.LogError("Trying to add <1 amount of resource");

            if (_resources.ContainsKey(type))
                _resources[type] += amount;
            else
                _resources[type] = amount;
        }

        public void RemoveResource(ResourceType type, int amount)
        {
            if (amount < 1)
                Debug.LogError("Trying to remove <1 amount of resource");
            if (amount > _resources[type])
                Debug.LogError("Trying to remove more than available");

            _resources[type] -= amount;
        }

        public bool HasResource(ResourceType type) =>
            _resources.ContainsKey(type) && _resources[type] > 0;
    }
}