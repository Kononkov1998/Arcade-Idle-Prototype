using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private Image _icon;

        [Header("Animation Tweaks")] 
        [SerializeField] private float _countChangeDuration = 0.5f;
        [SerializeField] private float _punchScale = 0.1f;
        [SerializeField] private float _punchDuration = 0.2f;

        public void Construct(int count, Sprite icon)
        {
            _count.text = count.ToString();
            _icon.sprite = icon;
        }

        public void Render(int newCount)
        {
            ShowHideIfNeeded(newCount);
            _count.text = newCount.ToString();
        }

        public void Render(int oldCount, int newCount)
        {
            ShowHideIfNeeded(newCount);
            DOTween.To(() => oldCount, x => _count.text = x.ToString(), newCount, _countChangeDuration);

            if (newCount > oldCount)
            {
                _count.transform.DORewind();
                _icon.transform.DORewind();
                _count.transform.DOPunchScale(Vector3.one * _punchScale, _punchDuration);
                _icon.transform.DOPunchScale(Vector3.one * _punchScale, _punchDuration);
            }
        }

        private void ShowHideIfNeeded(int newCount)
        {
            if (newCount == 0)
                gameObject.SetActive(false);
            else if (!gameObject.activeSelf && newCount > 0)
                gameObject.SetActive(true);
        }
    }
}