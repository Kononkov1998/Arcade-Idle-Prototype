using UniRx;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerMiner : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerCollision _collision;
        private ReactiveProperty<float> _stayingTime;

        public float TimeToHit => _collision.ResourceSourceInRange.TimeToHit;
        public IReadOnlyReactiveProperty<float> StayingTime => _stayingTime;

        private void Awake()
        {
            _stayingTime = new ReactiveProperty<float>();
        }

        private void Update()
        {
            if (!ResourceCanBeHit())
                return;

            UpdateStayingTime();

            if (CanHit())
            {
                _collision.ResourceSourceInRange.Hit();
                ResetStayingTime();
            }
        }

        private bool CanHit() =>
            _stayingTime.Value > _collision.ResourceSourceInRange.TimeToHit;

        private void ResetStayingTime() =>
            _stayingTime.Value = 0f;

        private void UpdateStayingTime()
        {
            if (_movement.IsStopped)
                _stayingTime.Value += Time.deltaTime;
            else if (_stayingTime.Value > 0f)
                _stayingTime.Value = 0f;
        }

        private bool ResourceCanBeHit() =>
            _collision.ResourceSourceInRange != null
            && _collision.ResourceSourceInRange.CanBeHit;
    }
}