using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private NavMeshAgent _agent;
        private Joystick _joystick;

        public bool IsStopped => _joystick.Direction == Vector2.zero;

        public void Init(Joystick joystick) => 
            _joystick = joystick;

        private void Update() => 
            Move();

        private void Move()
        {
            var moveDirection = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
            _agent.Move(_speed * moveDirection * Time.deltaTime);
        }
    }
}