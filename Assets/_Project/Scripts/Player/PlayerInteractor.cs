using _Project.Scripts.MinedResources;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerCollision _collision;
        private ReactiveProperty<float> _interactionProgress;
        private IActor _actor;

        public float TimeToHit => ResourceSourceInRange.TimeToInteract;
        public IReadOnlyReactiveProperty<float> InteractionProgress => _interactionProgress;
        private IInteractive ResourceSourceInRange => _collision.ResourceSourceInRange;

        public void Init(IActor actor)
        {
            _interactionProgress = new ReactiveProperty<float>();
            _actor = actor;
        }

        private void Update()
        {
            if (!ResourceCanBeInteracted())
                return;

            UpdateStayingTime();

            if (CanInteract())
            {
                ResourceSourceInRange.Interact(_actor);
                ResetStayingTime();
            }
        }

        private bool CanInteract() =>
            _interactionProgress.Value > ResourceSourceInRange.TimeToInteract;

        private void ResetStayingTime() =>
            _interactionProgress.Value = 0f;

        private void UpdateStayingTime()
        {
            if (_movement.IsStopped)
                _interactionProgress.Value += Time.deltaTime;
            else if (_interactionProgress.Value > 0f)
                _interactionProgress.Value = 0f;
        }

        private bool ResourceCanBeInteracted() =>
            ResourceSourceInRange != null && ResourceSourceInRange.CanInteract(_actor);
    }
}