using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.MinedResources;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Player
{
    [Serializable]
    public class Storage
    {
        [SerializeReference] private readonly ReactiveDictionary<ResourceType, int> _resources;

        public Storage() =>
            _resources = new ReactiveDictionary<ResourceType, int>();

        public Storage(Dictionary<ResourceType, int> startResources) =>
            _resources = new ReactiveDictionary<ResourceType, int>(startResources);

        public IReadOnlyReactiveDictionary<ResourceType, int> Resources => _resources;

        public void AddResource(ResourceType type, int amount)
        {
            if (_resources.ContainsKey(type))
                _resources[type] += amount;
            else
                _resources[type] = amount;
        }

        public void AddResources(IDictionary<ResourceType, int> resources)
        {
            foreach ((ResourceType type, int amount) in resources)
            {
                if (_resources.ContainsKey(type))
                    _resources[type] += amount;
                else
                    _resources[type] = amount;
            }
        }

        public void RemoveResource(ResourceType type, int amount) =>
            _resources[type] -= amount;

        public void Clear() =>
            _resources.Clear();

        public bool HasResource(ResourceType type, int minAmount = 1) =>
            _resources.ContainsKey(type) && _resources[type] >= minAmount;

        public bool Empty() =>
            _resources.Count == 0 || _resources.All(x => x.Value == 0);
    }
}