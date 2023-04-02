using _Project.Scripts.MinedResources.Factory;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Tutorial
{
    public class ResourceFactoryTutorial : TutorialStep
    {
        [SerializeField] private ResourceFactory _factory;
        private PlayerRoot _player;

        public override Vector3 TargetPosition => _factory.transform.position;
        public override bool Completed => !_factory.CanInteract(_player);

        public override void Construct(PlayerRoot player) =>
            _player = player;
    }
}