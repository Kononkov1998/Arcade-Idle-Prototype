using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] private Transform _pickUpPoint;

        public Transform PickUpPoint => _pickUpPoint;
        public Inventory Inventory { get; private set; }

        private void Awake()
        {
            Inventory = new Inventory();
        }
    }
}