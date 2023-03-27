using UnityEngine;

namespace _Project.Scripts.Input
{
    public interface IInput
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public Vector2 Direction { get; }
    }
}