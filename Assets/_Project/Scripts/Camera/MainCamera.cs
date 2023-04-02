using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public void Follow(Transform target) => 
            _virtualCamera.Follow = target;
    }
}