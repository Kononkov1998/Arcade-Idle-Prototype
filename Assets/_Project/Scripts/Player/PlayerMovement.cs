using _Project.Scripts.Services.Input;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private NavMeshAgent _agent;
        private IInput _input;

        public bool IsStopped => _input.Direction == Vector2.zero;

        public void Init(IInput input) => 
            _input = input;

        private void Update() => 
            Move();

        private void Move()
        {
            var moveDirection = new Vector3(_input.Horizontal, 0f, _input.Vertical);
            _agent.Move(_speed * moveDirection * Time.deltaTime);
        }
    }
}