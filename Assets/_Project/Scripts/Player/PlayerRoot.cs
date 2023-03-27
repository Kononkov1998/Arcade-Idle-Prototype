using _Project.Scripts.Data;
using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerRoot : MonoBehaviour, IPlayer
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerPickUp _pickUp;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PlayerInteractor _interactor;

        public Inventory Inventory => _inventory;

        public void Init(StaticData staticData, IInput input)
        {
            _movement.Init(input);
            _interactor.Init(this);
            _inventory.Init(_config, staticData);
            _pickUp.SetRadius(_config.PickUpRadius);
        }
    }

    public interface IPlayer
    {
        public Inventory Inventory { get; }
    }
}