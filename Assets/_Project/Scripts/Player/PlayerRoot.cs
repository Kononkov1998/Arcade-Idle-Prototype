using _Project.Scripts.MinedResources;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerRoot : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private PlayerPickUp _pickUp;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerInventory _inventory;

        public Inventory Inventory => _inventory.Inventory;

        public void Init(Joystick joystick)
        {
            _movement.Init(joystick);
            _pickUp.SetRadius(_config.PickUpRadius);
        }
    }
}