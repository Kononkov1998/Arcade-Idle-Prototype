using _Project.Scripts.MinedResources;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerPickUp : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private SphereCollider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Resource resource))
            {
                if (resource.CanBePickedUp)
                    PickUpResource(resource);
                else
                    resource.CanBePickedUpNow += PickUpOnAvailable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Resource resource))
                resource.CanBePickedUpNow -= PickUpOnAvailable;
        }

        public void SetRadius(float radius) =>
            _collider.radius = radius;

        private void PickUpOnAvailable(Resource resource)
        {
            resource.CanBePickedUpNow -= PickUpOnAvailable;
            PickUpResource(resource);
        }

        private void PickUpResource(Resource resource)
        {
            resource.OnPickedUp();
            Transform resourceTransform = resource.transform;
            resourceTransform.SetParent(_inventory.PickUpPoint);

            const float duration = 0.5f;
            DOTween.Sequence(resourceTransform)
                .Join(resourceTransform.DOLocalJump(Vector3.zero, 1f, 1, duration))
                .Join(resourceTransform.DOScale(Vector3.zero, duration).SetEase(Ease.InQuad))
                .OnComplete(() => _inventory.Storage.AddResource(resource.Type, resource.Amount));
        }
    }
}