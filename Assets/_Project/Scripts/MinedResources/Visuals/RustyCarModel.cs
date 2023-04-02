using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.MinedResources.Visuals
{
    public class RustyCarModel : MonoBehaviour
    {
        [Header("Punch")]
        [SerializeField] private float _punchScale = .2f;
        [SerializeField] private float _punchDuration = .2f;

        [Header("Show")]
        [SerializeField] private Vector3 _spawnPosition = new(0, 10f, 0);
        [SerializeField] private float _spawnResetScaleDuration = 0.25f;
        [SerializeField] private float _fallDuration = 0.5f;
        [SerializeField] private float _landPunch = 0.005f;
        [SerializeField] private float _landPunchDuration = 0.25f;
        [SerializeField] private ParticleSystem _landEffect;

        [Header("Hide")]
        [SerializeField] private Vector3 _hidePosition = new(0, -1.85f, 0);
        [SerializeField] private float _hideDuration = 1f;

        private Vector3 _startLocalPosition;
        private Vector3 _startScale;

        public bool Visible { get; private set; } = true;

        private void Awake()
        {
            _startLocalPosition = transform.localPosition;
            _startScale = transform.localScale;
        }

        public void Show()
        {
            Visible = true;
            transform.localScale = Vector3.zero;
            transform.localPosition = _spawnPosition;

            transform.DOScale(_startScale, _spawnResetScaleDuration);
            transform.DOLocalMove(_startLocalPosition, _fallDuration)
                .SetEase(Ease.InCubic)
                .OnComplete(() =>
                {
                    transform.DOPunchScale(Vector3.down * _landPunch, _landPunchDuration);
                    Instantiate(_landEffect, transform);
                });
        }

        public void Hide()
        {
            Visible = false;
            transform.DOLocalMove(_hidePosition, _hideDuration);
        }

        public void PunchScale() =>
            transform.DOPunchScale(Vector3.one * _punchScale, _punchDuration);
    }
}