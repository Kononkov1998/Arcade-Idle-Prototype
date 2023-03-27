using Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera VirtualCamera;

        public void Init(Transform followTarget)
        {
            VirtualCamera.Follow = followTarget;
        }
    }
}