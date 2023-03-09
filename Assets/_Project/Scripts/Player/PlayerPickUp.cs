using _Project.Scripts.MinedResources;
using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerPickUp : MonoBehaviour
    {
        [SerializeField] private PlayerInventory _playerInventory;
        [SerializeField] private SphereCollider _collider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Resource resource))
            {
                if (resource.CanBePickedUp)
                    PickUpResource(resource);
                else
                    resource.CanBePickedUpNow += PickUpWhenAvailable;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Resource resource))
                resource.CanBePickedUpNow -= PickUpWhenAvailable;
        }

        public void SetRadius(float radius) =>
            _collider.radius = radius;

        private void PickUpWhenAvailable(Resource resource)
        {
            resource.CanBePickedUpNow -= PickUpWhenAvailable;
            PickUpResource(resource);
        }

        private void PickUpResource(Resource resource)
        {
            resource.OnPickedUp();
            Transform resourceTransform = resource.transform;
            resourceTransform.SetParent(_playerInventory.PickUpPoint);

            const float duration = 0.5f;
            DOTween.Sequence(resourceTransform)
                .Join(resourceTransform.DOLocalJump(Vector3.zero, 1f, 1, duration))
                .Join(resourceTransform.DOScale(Vector3.zero, duration).SetEase(Ease.InQuad))
                .OnComplete(() => _playerInventory.Inventory.AddResource(resource.Type, resource.Amount));
        }
    }
}