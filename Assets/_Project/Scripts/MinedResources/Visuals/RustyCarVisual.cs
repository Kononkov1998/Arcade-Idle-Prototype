using UniRx;
using UnityEngine;

namespace _Project.Scripts.MinedResources.Visuals
{
    public class RustyCarVisual : MonoBehaviour
    {
        [SerializeField] private ResourceSource _source;
        [SerializeField] private RustyCarModel _model;
        [SerializeField] private ParticleSystem _hitEffect;

        private int _durability;

        private void Start()
        {
            _durability = _source.Durability.Value;
            _source.Durability.Subscribe(OnDurabilityChanged);
        }

        private void OnDurabilityChanged(int newDurability)
        {
            if (newDurability < _durability)
            {
                Instantiate(_hitEffect, transform);
                if (newDurability > 0)
                    _model.PunchScale();
                else if (_model.Visible)
                    _model.Hide();
            }
            else if (newDurability == _source.MaxDurability && !_model.Visible)
            {
                _model.Show();
            }

            _durability = newDurability;
        }
    }
}