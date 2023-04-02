using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Data;
using _Project.Scripts.MinedResources;
using _Project.Scripts.MinedResources.Factory;
using _Project.Scripts.MinedResources.Spawner;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class FactoryView : MonoBehaviour
    {
        [SerializeField] private ResourceFactory _factory;
        [SerializeField] private ResourceSpawner _spawner;
        [SerializeField] private float _distanceBetweenViews = .3f;
        [SerializeField] private Image _progressBackground;
        [SerializeField] private Image _progress;

        private StaticData _staticData;
        private Dictionary<ResourceType, ResourceView> _resourceViews;

        public void Construct(StaticData staticData) => 
            _staticData = staticData;

        public void Init()
        {
            _resourceViews = new Dictionary<ResourceType, ResourceView>();
            List<ResourceType> keys = _factory.StartNeededResources.Keys.ToList();
            
            _factory.CreationProgress.Subscribe(OnCreationProgressChanged);
            SetSprites();
            for (var i = 0; i < keys.Count; i++)
                CreateResourceView(keys, i);
        }

        private void SetSprites()
        {
            ResourceConfig config = _staticData.GetResourceConfig(_spawner.ResourceType);
            _progressBackground.sprite = config.WhiteIcon;
            _progress.sprite = config.Icon;
        }

        private void CreateResourceView(IReadOnlyList<ResourceType> keys, int index)
        {
            ResourceType type = keys[index];
            ResourceConfig config = _staticData.GetResourceConfig(type);
            ResourceView view = Instantiate(_staticData.NeededResourceViewPrefab, transform);

            view.Init(_factory.StartNeededResources[config.Type], config.Icon);
            CalculateNewPosition(view.transform, index, _factory.StartNeededResources.Count);

            _factory.NeededResources.Resources.ObserveAdd().Subscribe(OnAdd);
            _factory.NeededResources.Resources.ObserveReplace().Subscribe(OnReplace);

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
            GameObject progressBackgroundObject = _progressBackground.gameObject;
            if (progressBackgroundObject.activeSelf && progress == 0f)
                progressBackgroundObject.SetActive(false);
            else if (!progressBackgroundObject.activeSelf && progress > 0f)
                progressBackgroundObject.SetActive(true);

            _progress.fillAmount = progress;
        }
    }
}