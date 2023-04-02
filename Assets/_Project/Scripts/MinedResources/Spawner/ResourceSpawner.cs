using _Project.Scripts.Data;
using _Project.Scripts.Extensions;
using UnityEngine;

namespace _Project.Scripts.MinedResources.Spawner
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private ResourceSpawnerConfig _config;
        [SerializeField] private float _spawnConeRadius = 0.2f;
        [SerializeField] private float _spawnConeHeight = 1f;
        private Resource _resourcePrefab;

        public ResourceType ResourceType => _resourceType;

        public void Construct(StaticData staticData) =>
            _resourcePrefab = staticData.GetResourcePrefab(_resourceType, _config.AmountInResourceObject);

        public void Spawn()
        {
            for (var i = 0; i < _config.ResourcesCount; i++)
            {
                Resource resource = Instantiate(_resourcePrefab, transform.position, Quaternion.identity);
                float x = Random.Range(-_spawnConeRadius, _spawnConeRadius);
                var xzOffset = new Vector2(x, -_spawnConeRadius);
                Vector3 direction = new Vector3(xzOffset.x, _spawnConeHeight, xzOffset.y).normalized;
                float randomForce = Random.Range(_config.MinResourceForce, _config.MaxResourceForce);

                Vector3 force = direction * randomForce;
                Vector3 torqueForce = VectorFactory.Create(randomForce);

                resource.Init(_resourceType, _config.AmountInResourceObject, _config.ResourcePickUpDelay);
                resource.Physics.AddForce(force, torqueForce);
            }
        }
    }
}