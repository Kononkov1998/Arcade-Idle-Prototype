using _Project.Scripts.MinedResources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private Image _icon;
        public ResourceType ResourceType { get; private set; }

        public void Init(ResourceType resourceType, int count, Sprite icon)
        {
            ResourceType = resourceType;
            _count.text = count.ToString();
            _icon.sprite = icon;
        }

        public void Render(int newCount)
        {
            _count.text = newCount.ToString();
        }
    }
}