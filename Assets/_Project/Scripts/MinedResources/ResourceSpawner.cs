using _Project.Scripts.Data;
using _Project.Scripts.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.MinedResources
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private ResourceSpawnerConfig _config;

        private Resource _resourcePrefab;

        public void Init(StaticData staticData) =>
            _resourcePrefab = staticData.GetResourcePrefab(_resourceType, _config.AmountInResourceObject);

        public void Spawn()
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