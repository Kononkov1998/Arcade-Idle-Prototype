using System.Collections;
using _Project.Scripts.Data;
using _Project.Scripts.Player;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    public class ResourceSource : MonoBehaviour, IInteractive
    {
        [SerializeField] private ResourceSourceId _id;
        [SerializeField] private ResourceSourceConfig _config;
        [SerializeField] private ResourceSpawner _spawner;
        private ReactiveProperty<int> _durability;

        public bool CanInteract(IPlayer _) => _durability.Value > 0;
        public IReadOnlyReactiveProperty<int> Durability => _durability;
        public int MaxDurability => _config.Durability;
        public ResourceSourceId Id => _id;
        public float TimeToInteract { get; private set; }

        private void Awake()
        {
            _durability = new ReactiveProperty<int>(_config.Durability);
            TimeToInteract = 1f / _config.HitsPerSecond;
        }

        public void Interact(IPlayer _)
        {
            SpawnResources();
            _durability.Value--;
            if (_durability.Value == 0)
                StartCoroutine(RecoveryRoutine());
        }

        private IEnumerator RecoveryRoutine()
        {
            yield return new WaitForSeconds(_config.RecoveryDuration);
            _durability.Value = _config.Durability;
        }

        private void SpawnResources() =>
            _spawner.Spawn();
    }
}