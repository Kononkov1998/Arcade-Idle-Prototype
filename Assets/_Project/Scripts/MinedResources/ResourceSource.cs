using System.Collections;
using _Project.Scripts.Data;
using _Project.Scripts.Extensions;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.MinedResources
{
    public class ResourceSource : MonoBehaviour
    {
        [SerializeField] private ResourceSourceType _type;
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private Resource _resourcePrefab;
        [SerializeField] private ResourceSourceConfig _config;
        private ReactiveProperty<int> _durability;

        public bool CanBeHit => _durability.Value > 0;
        public IReadOnlyReactiveProperty<int> Durability => _durability;
        public int MaxDurability => _config.Durability;
        public ResourceSourceType Type => _type;
        public float TimeToHit { get; private set; }

        private void Awake()
        {
            _durability = new ReactiveProperty<int>(_config.Durability);
            TimeToHit = 1f / _config.HitsPerSecond;
        }

        public void Hit()
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

        private void SpawnResources()
        {
            for (var i = 0; i < _config.ResourcesCount; i++)
            {
                Resource resource = Instantiate(_resourcePrefab, transform.position, Quaternion.identity);
                const float coneRadius = 0.2f;
                const float coneHeight = 1f;
                Vector2 xzOffset = Random.insideUnitCircle * coneRadius;
                Vector3 direction = new Vector3(xzOffset.x, coneHeight, xzOffset.y).normalized;
                float randomForce = Random.Range(_config.MinResourceForce, _config.MaxResourceForce);

                Vector3 force = direction * randomForce;
                Vector3 torqueForce = VectorFactory.Create(randomForce);

                resource.Init(_resourceType, _config.AmountInResourceObject, _config.ResourcePickUpDelay);
                resource.Physics.AddForce(force, torqueForce);
            }
        }
    }
}