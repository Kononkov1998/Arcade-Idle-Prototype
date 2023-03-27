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

        private void ShowHideIfNeeded(int newCount)
        {
            if (newCount == 0)
                gameObject.SetActive(false);
            else if (!gameObject.activeSelf && newCount > 0)
                gameObject.SetActive(true);
        }
    }
}