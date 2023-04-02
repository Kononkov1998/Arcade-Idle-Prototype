using System;
using System.Collections.Generic;
using _Project.Scripts.Data;
using _Project.Scripts.MinedResources;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Player
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Transform _pickUpPoint;
        [SerializeField] private Transform _transferHoldPoint;
        private PlayerConfig _config;
        private StaticData _staticData;

        public Transform PickUpPoint => _pickUpPoint;
        public Storage Storage { get; private set; }

        public void Construct(PlayerConfig config, StaticData staticData, Dictionary<ResourceType, int> resources)
        {
            _config = config;
            _staticData = staticData;
            Storage = new Storage(resources);
        }

        public void TransferResource(ResourceType type, Vector3 targetPosition, Action onComplete)
        {
            Transform resource = CreateResource(type).transform;
            Vector3 randomHoldPosition = _transferHoldPoint.position;
            randomHoldPosition.x += Random.Range(-_config.SpreadRadius, _config.SpreadRadius);
            randomHoldPosition.z += Random.Range(-_config.SpreadRadius, _config.SpreadRadius);

            DOTween.Sequence()
                .Append(resource.DOJump(randomHoldPosition, 1f, 1, _config.TransferToHoldPointDuration))
                .Join(resource.DOScale(Vector3.one * 0.5f, _config.TransferToHoldPointDuration))
                .AppendInterval(_config.TransferDelay)
                .Append(resource.DOJump(targetPosition, 1f, 1, _config.TransferDuration))
                .Join(resource.DOScale(Vector3.zero, _config.TransferDuration))
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                    Destroy(resource.gameObject);
                });
        }

        private Resource CreateResource(ResourceType type)
        {
            const int resourceAmountInOneObject = 1;
            Resource resource = Instantiate(
                _staticData.GetResourceConfig(type).GetResourcePrefab(resourceAmountInOneObject),
                PickUpPoint.position,
                Quaternion.identity
            );
            resource.Physics.SetKinematic();
            resource.Physics.DisableCollider();
            resource.transform.localScale = Vector3.zero;
            return resource;
        }
    }
}