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
        private IPlayer _player;

        public float TimeToHit => ResourceSourceInRange.TimeToInteract;
        public IReadOnlyReactiveProperty<float> InteractionProgress => _interactionProgress;
        private IInteractive ResourceSourceInRange => _collision.ResourceSourceInRange;

        public void Init(IPlayer player)
        {
            _interactionProgress = new ReactiveProperty<float>();
            _player = player;
        }

        private void Update()
        {
            if (!ResourceCanBeHit())
                return;

            UpdateStayingTime();

            if (CanHit())
            {
                ResourceSourceInRange.Interact(_player);
                ResetStayingTime();
            }
        }

        private bool CanHit() =>
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

        private bool ResourceCanBeHit() =>
            ResourceSourceInRange != null && ResourceSourceInRange.CanInteract(_player);
    }
}