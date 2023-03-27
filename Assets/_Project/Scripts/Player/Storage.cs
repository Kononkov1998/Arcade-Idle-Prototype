using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.MinedResources;
using UniRx;

namespace _Project.Scripts.Player
{
    public class Storage
    {
        private readonly ReactiveDictionary<ResourceType, int> _resources;
        public IReadOnlyReactiveDictionary<ResourceType, int> Resources => _resources;

        public Storage() =>
            _resources = new ReactiveDictionary<ResourceType, int>();

        public Storage(Dictionary<ResourceType, int> startResources) =>
            _resources = new ReactiveDictionary<ResourceType, int>(startResources);

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

        public bool HasResource(ResourceType type) =>
            _resources.ContainsKey(type) && _resources[type] > 0;

        public bool Empty() =>
            _resources.Count == 0 || _resources.All(x => x.Value == 0);
    }
}