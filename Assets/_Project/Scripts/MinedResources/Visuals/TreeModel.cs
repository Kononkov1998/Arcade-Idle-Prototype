using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.MinedResources.Visuals
{
    public class TreeModel : MonoBehaviour
    {
        [SerializeField] private float _punchScale = .2f;
        [SerializeField] private float _punchDuration = .2f;
        [SerializeField] private float _growDuration = 0.5f;
        [SerializeField] private Vector3 _fallRotation = new(70, 0, 0);
        [SerializeField] private float _fallDuration = 0.5f;
        [SerializeField] private Vector3 _hidePosition = new(0, -2.6f, 0);
        [SerializeField] private float _hideDuration = 0.5f;

        private Transform _transform;
        private Vector3 _startScale;

        public bool Visible { get; private set; } = true;

        private void Awake()
        {
            _transform = transform;
            _startScale = _transform.localScale;
        }

        public void Show()
        {
            Visible = true;
            _transform.localRotation = Quaternion.identity;
            _transform.localPosition = Vector3.zero;
            _transform.localScale = Vector3.zero;
            _transform.DOScale(_startScale, _growDuration);
        }
[SerializeField] private ParticleSystem _landEffect;
        public void Hide()
        {
            Visible = false;
            DOTween.Sequence()
                .Append(_transform.DOLocalRotate(_fallRotation, _fallDuration))
                .Append(_transform.DOLocalMove(_hidePosition, _hideDuration));
        }

        public void PunchScale() =>
            _transform.DOPunchScale(Vector3.one * _punchScale, _punchDuration);
    }
}