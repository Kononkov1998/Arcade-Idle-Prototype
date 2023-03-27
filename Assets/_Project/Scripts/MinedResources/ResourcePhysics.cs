using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    public class ResourcePhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;

        public void AddForce(Vector3 force, Vector3 torqueForce)
        {
            _rigidbody.AddForce(force);
            _rigidbody.AddTorque(torqueForce);
        }

        public void SetKinematic() =>
            _rigidbody.isKinematic = true;

        public void DisableCollider() =>
            _collider.enabled = false;
    }
}