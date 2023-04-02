using _Project.Scripts.MinedResources;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerCollision _collision;
        private IActor _actor;
        private ReactiveProperty<float> _interactionProgress;

        public float TimeToHit => ResourceSourceInRange.TimeToInteract;
        public IReadOnlyReactiveProperty<float> InteractionProgress => _interactionProgress;
        private IInteractive ResourceSourceInRange => _collision.ResourceSourceInRange;

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

        public void Construct(IActor actor)
        {
            _interactionProgress = new ReactiveProperty<float>();
            _actor = actor;
        }

        private bool CanInteract() =>
            _interactionProgress.Value > ResourceSourceInRange.TimeToInteract;

        private void ResetStayingTime() =>
            _interactionProgress.Value = 0f;

        private void UpdateStayingTime()
        {
            if (!_movement.HasInput)
                _interactionProgress.Value += Time.deltaTime;
            else if (_interactionProgress.Value > 0f)
                _interactionProgress.Value = 0f;
        }

        private bool ResourceCanBeInteracted() =>
            ResourceSourceInRange != null && ResourceSourceInRange.CanInteract(_actor);
    }
}