using _Project.Scripts.MinedResources.Source;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.MinedResources.Visuals
{
    public class CrystalVisual : MonoBehaviour
    {
        [SerializeField] private ResourceSource _source;
        [SerializeField] private ParticleSystem _hitEffect;
        [SerializeField] private Transform _model;
        [SerializeField] private BreakableCrystal[] _breakableCrystals;
        [Header("Animation Tweaks")]
        [SerializeField] private float _punchScale = .2f;
        [SerializeField] private float _punchDuration = .2f;
        [SerializeField] private float _timeToShowBreakableCrystal = 0.25f;
        [SerializeField] private float _timeToHideBreakableCrystal = 0.05f;

        private int _durability;

        private void Start()
        {
            _durability = _source.Durability.Value;
            InitCrystals();
            _source.Durability.Subscribe(OnDurabilityChanged);
        }

        private void InitCrystals()
        {
            float durabilityForCrystal = (float) _source.MaxDurability / _breakableCrystals.Length;
            for (var i = 0; i < _breakableCrystals.Length; i++)
            {
                float thresholdForHide = durabilityForCrystal * (i + 1);
                _breakableCrystals[i].Init(thresholdForHide, _timeToShowBreakableCrystal, _timeToHideBreakableCrystal);
            }
        }

        private void OnDurabilityChanged(int newDurability)
        {
            if (newDurability < _durability)
            {
                Instantiate(_hitEffect, transform);
                _model.DOPunchScale(Vector3.one * _punchScale, _punchDuration);
                HideBrokenCrystals(newDurability);
            }
            else if (newDurability == _source.MaxDurability)
            {
                ShowAllCrystals();
            }

            _durability = newDurability;
        }

        private void HideBrokenCrystals(int newDurability)
        {
            foreach (BreakableCrystal crystal in _breakableCrystals)
                if (crystal.Visible && crystal.ThresholdForHide > newDurability)
                    crystal.Hide();
        }

        private void ShowAllCrystals()
        {
            foreach (BreakableCrystal crystal in _breakableCrystals)
                if (!crystal.Visible)
                    crystal.Show();
        }
    }
}