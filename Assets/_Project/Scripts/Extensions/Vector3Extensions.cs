using UnityEngine;

namespace _Project.Scripts.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 RandomPointOnLine(this Vector3 from, Vector3 to) =>
            Vector3.Lerp(from, to, Random.Range(0, 1f));

        public static void Set(this ref Vector3 vector3, float value) =>
            vector3.Set(value, value, value);

        public static void SetX(this ref Vector3 vector3, float value) =>
            vector3.Set(value, vector3.y, vector3.z);

        public static void SetY(this ref Vector3 vector3, float value) =>
            vector3.Set(vector3.x, value, vector3.z);

        public static void SetZ(this ref Vector3 vector3, float value) =>
            vector3.Set(vector3.x, vector3.y, value);
    }
}