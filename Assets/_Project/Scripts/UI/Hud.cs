using System.Collections.Generic;
using _Project.Scripts.MinedResources;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private RectTransform _resourcesParent;

        public RectTransform ResourcesParent => _resourcesParent;
        private Dictionary<ResourceType, ResourceView> _resourceViews;

        public void Init(IReadOnlyReactiveDictionary<ResourceType, int> resources)
        {
            _resourceViews = new Dictionary<ResourceType, ResourceView>();
            resources.ObserveAdd().Subscribe(OnResourceAdd);
            resources.ObserveReplace().Subscribe(OnResourceReplace);
        }

        public void AddResourceView(ResourceType type, ResourceView view) => 
            _resourceViews.Add(type, view);

        private void OnResourceAdd(DictionaryAddEvent<ResourceType, int> addEvent) => 
            _resourceViews[addEvent.Key].Render(addEvent.Value);

        private void OnResourceReplace(DictionaryReplaceEvent<ResourceType, int> replaceEvent) => 
            _resourceViews[replaceEvent.Key].Render(replaceEvent.NewValue);
    }
}