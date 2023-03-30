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

        public void Init(int count, Sprite icon)
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
            DOTween.To(() => oldCount, x => _count.text = x.ToString(), newCount, .5f);

            if (newCount > oldCount)
            {
                _count.transform.DORewind();
                _icon.transform.DORewind();
                _count.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
                _icon.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
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