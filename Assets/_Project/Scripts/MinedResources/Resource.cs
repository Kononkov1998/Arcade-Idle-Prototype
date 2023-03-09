using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.MinedResources
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _amount;
        [SerializeField] private bool _canBePickedUp;
        [SerializeField] private ResourcePhysics _physics;

        public ResourceType Type => _type;
        public int Amount => _amount;
        public bool CanBePickedUp => _canBePickedUp;
        public ResourcePhysics Physics => _physics;

        public event Action<Resource> CanBePickedUpNow;

        public void Init(ResourceType type, int amount, float pickUpDelay)
        {
            _type = type;
            _amount = amount;
            StartCoroutine(ActivatePickUpAfterDelay(pickUpDelay));
        }

        public void OnPickedUp()
        {
            _canBePickedUp = false;
            _physics.SetKinematic();
        }

        private IEnumerator ActivatePickUpAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _canBePickedUp = true;
            CanBePickedUpNow?.Invoke(this);
        }
    }
}