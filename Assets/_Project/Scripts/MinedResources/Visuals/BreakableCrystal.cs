using DG.Tweening;
using UnityEngine;

namespace _Project.Scripts.MinedResources.Visuals
{
    public class BreakableCrystal : MonoBehaviour
    {
        private float _timeToShow;
        private float _timeToHide;
        private Vector3 _startScale;

        public float ThresholdForHide { get; private set; }
        public bool Visible { get; private set; } = true;

        public void Init(float thresholdForHide, float timeToShow, float timeToHide)
        {
            _startScale = transform.localScale;
            ThresholdForHide = thresholdForHide;
            _timeToShow = timeToShow;
            _timeToHide = timeToHide;
        }

        public void Show()
        {
            Visible = true;
            transform.DOScale(_startScale, _timeToShow);
        }

        public void Hide()
        {
            Visible = false;
            transform.DOScale(Vector3.zero, _timeToHide);
        }
    }
}