using UnityEngine;

namespace _Project.Scripts.Extensions
{
    public static class ComponentExtensions
    {
        public static bool IsPrefab(this Component value) => 
            value.gameObject.scene.rootCount == 0;
    }
}