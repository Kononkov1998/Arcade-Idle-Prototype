using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Data;
using _Project.Scripts.MinedResources;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class FactoryView : MonoBehaviour
    {
        [SerializeField] private ResourceFactory _factory;
        [SerializeField] private float _distanceBetweenViews = .3f;
        [SerializeField] private GameObject _progressBackground;
        [SerializeField] private Image _progress;

        private Dictionary<ResourceType, ResourceView> _resourceViews;

        public void Init(StaticData staticData)
        {
            _resourceViews = new Dictionary<ResourceType, ResourceView>();
            List<ResourceType> keys = _factory.StartNeededResources.Keys.ToList();

            for (var i = 0; i < keys.Count; i++)
                CreateResourceView(staticData, keys, i);
        }

        private void CreateResourceView(StaticData staticData, IReadOnlyList<ResourceType> keys, int index)
        {
            ResourceType type = keys[index];
            ResourceConfig config = staticData.GetResourceConfig(type);
            ResourceView view = Instantiate(staticData.NeededResourceViewPrefab, transform);

            view.Init(_factory.StartNeededResources[config.Type], config.Icon);
            CalculateNewPosition(view.transform, index, _factory.StartNeededResources.Count);

            _factory.NeededResources.Resources.ObserveAdd().Subscribe(OnAdd);
            _factory.NeededResources.Resources.ObserveReplace().Subscribe(OnReplace);
            _factory.CreationProgress.Subscribe(OnCreationProgressChanged);

            _resourceViews.Add(type, view);
        }

        private void CalculateNewPosition(Transform view, int index, int count)
        {
            float yOffset = -_distanceBetweenViews / 2f * (count - 1);
            float y = (count - index - 1) * _distanceBetweenViews + yOffset;
            Vector3 newPosition = view.localPosition;
            newPosition.y = y;
            view.localPosition = newPosition;
        }

        private void OnAdd(DictionaryAddEvent<ResourceType, int> addEvent) =>
            _resourceViews[addEvent.Key].Render(addEvent.Value);

        private void OnReplace(DictionaryReplaceEvent<ResourceType, int> replaceEvent) =>
            _resourceViews[replaceEvent.Key].Render(replaceEvent.NewValue);

        private void OnCreationProgressChanged(float progress)
        {
            if (_progressBackground.activeSelf && progress == 0f)
                _progressBackground.SetActive(false);
            else if (!_progressBackground.activeSelf && progress > 0f)
                _progressBackground.SetActive(true);

            _progress.fillAmount = progress;
        }
    }
}