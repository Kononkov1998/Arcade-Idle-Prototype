using UnityEngine;

namespace _Project.Scripts.Extensions
{
    public static class VectorFactory
    {
        public static Vector3 Create(float value) => 
            new(value, value, value);

        public static Vector3 CreateY(Vector3 origin, float y) =>
            new(origin.x, y, origin.z);
    }
}