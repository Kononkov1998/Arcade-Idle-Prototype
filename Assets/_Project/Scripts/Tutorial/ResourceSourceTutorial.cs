using System.Linq;
using _Project.Scripts.MinedResources.Factory;
using _Project.Scripts.MinedResources.Source;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Tutorial
{
    public class ResourceSourceTutorial : TutorialStep
    {
        [SerializeField] private ResourceSource _source;
        [SerializeField] private ResourceFactory _factory;
        private Storage _playerStorage;

        public override Vector3 TargetPosition => _source.transform.position;
        public override bool Completed => HasResourcesForFactory();

        public override void Construct(PlayerRoot player) =>
            _playerStorage = player.Inventory.Storage;

        private bool HasResourcesForFactory() =>
            _factory.NeededResources.Resources.All(neededResource =>
                _playerStorage.HasResource(neededResource.Key, neededResource.Value));
    }
}