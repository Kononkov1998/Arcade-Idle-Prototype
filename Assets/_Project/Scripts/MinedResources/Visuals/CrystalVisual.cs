using System;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.MinedResources.Visuals
{
    public class CrystalVisual : MonoBehaviour
    {
        private const string MaterialColorName = "_ColorTint1";
    
        [SerializeField] private ResourceSource _source;
        [SerializeField] private Material _material;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        private IDisposable _disposable;

        private void Start() => 
            _disposable = _source.Durability.Subscribe(OnDurabilityChanged);


        private void OnDestroy() => 
            _disposable?.Dispose();

        private void OnDurabilityChanged(int durability)
        {
            float durabilityPercent = (float) durability / _source.MaxDurability;
            UpdateColor(durabilityPercent);
        }

        private void UpdateColor(float durabilityPercent)
        {
            Color newColor = Color.Lerp(_inactiveColor, _activeColor, durabilityPercent);
            _material.DOColor(newColor, MaterialColorName, 0.1f);
        }
    }
}