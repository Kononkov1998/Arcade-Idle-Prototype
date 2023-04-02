using System.Collections.Generic;
using _Project.Scripts.Data;
using _Project.Scripts.MinedResources;
using _Project.Scripts.Services.Input;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerRoot : MonoBehaviour, IActor
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerPickUp _pickUp;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PlayerInteractor _interactor;

        public Inventory Inventory => _inventory;

        public void Construct(StaticData staticData, IInput input, Dictionary<ResourceType, int> resources)
        {
            _movement.Construct(input);
            _interactor.Construct(this);
            _inventory.Construct(_config, staticData, resources);
            _pickUp.SetRadius(_config.PickUpRadius);
        }
    }
}