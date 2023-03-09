using _Project.Scripts.MinedResources;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        public ResourceSource ResourceSourceInRange { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ResourceSource resourceSource))
                ResourceSourceInRange = resourceSource;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ResourceSource resourceSource) && resourceSource == ResourceSourceInRange)
                ResourceSourceInRange = null;
        }
    }
}