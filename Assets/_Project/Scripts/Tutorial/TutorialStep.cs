using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Tutorial
{
    public abstract class TutorialStep : MonoBehaviour
    {
        public abstract Vector3 TargetPosition { get; }
        public abstract bool Completed { get; }

        public abstract void Construct(PlayerRoot player);
    }
}