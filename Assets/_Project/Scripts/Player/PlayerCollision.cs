using _Project.Scripts.MinedResources;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        public IInteractive ResourceSourceInRange { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractive resourceSource))
                ResourceSourceInRange = resourceSource;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractive resourceSource) && resourceSource == ResourceSourceInRange)
                ResourceSourceInRange = null;
        }
    }
}