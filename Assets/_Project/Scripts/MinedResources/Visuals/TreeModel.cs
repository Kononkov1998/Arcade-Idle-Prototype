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
        private Vector3 _startScale;

        public bool Visible { get; private set; } = true;

        private void Awake() =>
            _startScale = transform.localScale;

        public void Show()
        {
            Visible = true;
            transform.localRotation = Quaternion.identity;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.zero;
            transform.DOScale(_startScale, _growDuration);
        }

        public void Hide()
        {
            Visible = false;
            DOTween.Sequence()
                .Append(transform.DOLocalRotate(_fallRotation, _fallDuration))
                .Append(transform.DOLocalMove(_hidePosition, _hideDuration));
        }

        public void PunchScale() =>
            transform.DOPunchScale(Vector3.one * _punchScale, _punchDuration);
    }
}