using _Project.Scripts.Extensions;
using _Project.Scripts.Player;
using NaughtyAttributes;
using OdinSerializer.Utilities;
using UnityEngine;

namespace _Project.Scripts.Tutorial
{
    public class StartTutorial : MonoBehaviour
    {
        [SerializeField] private Arrow _arrow;
        [SerializeField] private float _arrowHideSqrDistance = 11f;
        [SerializeField] private TutorialStep[] _steps;

        private PlayerRoot _player;
        private int _currentStepIndex;
        private bool _enabled;

        private TutorialStep CurrentStep => _steps[_currentStepIndex];

        public void Construct(PlayerRoot player)
        {
            _player = player;
            _steps.ForEach(x => x.Construct(_player));
        }

        public void Enable()
        {
            _arrow.AttachTo(_player.transform);
            _enabled = true;
        }

        private void Update()
        {
            if (!_enabled)
                return;
            
            HandleArrowVisibility();
            RotateArrowToTarget();

            if (CurrentStep.Completed)
            {
                if (HasNextStep())
                    SelectNextStep();
                else
                    EndTutorial();
            }
        }

        private void HandleArrowVisibility()
        {
            float sqrDistanceToTarget = Vector3.SqrMagnitude(CurrentStep.TargetPosition - _arrow.transform.position);
            if (_arrow.Visible && sqrDistanceToTarget < _arrowHideSqrDistance)
            {
                _arrow.Hide();
            }
            else if (!_arrow.Visible && sqrDistanceToTarget > _arrowHideSqrDistance)
            {
                _arrow.Show();
            }
        }

        private void RotateArrowToTarget()
        {
            Vector3 direction = VectorFactory.CreateY(CurrentStep.TargetPosition, _arrow.transform.position.y);
            _arrow.transform.LookAt(direction);
        }

        private bool HasNextStep() =>
            _currentStepIndex + 1 < _steps.Length;

        private void SelectNextStep() =>
            _currentStepIndex++;

        private void EndTutorial()
        {
            Destroy(_arrow.gameObject);
            Destroy(gameObject);
        }

        [Button]
        private void CollectStepsInChildren() => 
            _steps = GetComponentsInChildren<TutorialStep>();
    }
}