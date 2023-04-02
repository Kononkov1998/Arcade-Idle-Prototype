using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.MinedResources.Spawner;
using _Project.Scripts.Player;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.MinedResources.Factory
{
    public class ResourceFactory : MonoBehaviour, IInteractive
    {
        private const int ResourceAmountInOneObject = 1;

        [SerializeField] private ResourceFactoryConfig _config;
        [SerializeField] private ResourceSpawner _spawner;
        [SerializeField] private Transform _resourceInputPoint;

        private Storage _resourcesInTransfer;
        private IReactiveProperty<float> _creationProgress;

        public Storage NeededResources { get; private set; }
        public float TimeToInteract { get; private set; }
        public IReadOnlyReactiveProperty<float> CreationProgress => _creationProgress;
        public IDictionary<ResourceType, int> StartNeededResources => _config.NeededResources;

        public void Construct()
        {
            _creationProgress = new FloatReactiveProperty();
            _resourcesInTransfer = new Storage();
            NeededResources = new Storage(new Dictionary<ResourceType, int>(_config.NeededResources));
            TimeToInteract = 1f / _config.TransfersPerSecond;
        }

        private void FillNeededResources()
        {
            NeededResources.Clear();
            NeededResources.AddResources(_config.NeededResources);
        }

        private void SpawnResources() =>
            _spawner.Spawn();

        public bool CanInteract(IActor actor)
        {
            foreach ((ResourceType resourceType, int neededAmount) in NeededResources.Resources)
                if (CanBeTransferredAmount(actor.Inventory.Storage, resourceType, neededAmount) > 0)
                    return true;

            return false;
        }

        private int CanBeTransferredAmount(Storage storage, ResourceType resourceType, int neededAmount)
        {
            int amountInTransfer = _resourcesInTransfer.Resources.FirstOrDefault(x => x.Key == resourceType).Value;
            int canBeTransferredAmount = neededAmount - amountInTransfer;
            if (canBeTransferredAmount > 0 && storage.HasResource(resourceType))
                return canBeTransferredAmount;
            return 0;
        }

        private ResourceType GetNeededResourceData(Storage storage)
        {
            foreach ((ResourceType resourceType, int neededAmount) in NeededResources.Resources)
            {
                int canBeTransferredAmount = CanBeTransferredAmount(storage, resourceType, neededAmount);
                if (canBeTransferredAmount > 0)
                    return resourceType;
            }

            throw new InvalidOperationException("No needed resource found");
        }

        public void Interact(IActor actor)
        {
            ResourceType type = GetNeededResourceData(actor.Inventory.Storage);
            Inventory inventory = actor.Inventory;

            inventory.Storage.RemoveResource(type, ResourceAmountInOneObject);
            _resourcesInTransfer.AddResource(type, ResourceAmountInOneObject);

            inventory.TransferResource(type, _resourceInputPoint.position,
                () => ResourceTransferEnded(type, ResourceAmountInOneObject));
        }

        private void ResourceTransferEnded(ResourceType type, int amount)
        {
            _resourcesInTransfer.RemoveResource(type, amount);
            NeededResources.RemoveResource(type, amount);
            if (NeededResources.Empty())
                StartCoroutine(SpawnResourcesAfterDelay());
        }

        private IEnumerator SpawnResourcesAfterDelay()
        {
            var timePassed = 0f;
            while (timePassed < _config.ItemCreationDuration)
            {
                timePassed += Time.deltaTime;
                _creationProgress.Value = timePassed / _config.ItemCreationDuration;
                yield return null;
            }

            _creationProgress.Value = 0f;
            SpawnResources();
            FillNeededResources();
        }
    }
}