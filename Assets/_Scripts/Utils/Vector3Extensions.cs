using UnityEngine;

namespace Utils
{
    public static class Vector3Extensions
    {
        public static Vector3 Clamped(this Vector3 vector, float min, float max)
        {
            var clampedMagnitude = Mathf.Clamp(vector.magnitude, min, max);
            return vector.normalized * clampedMagnitude;
        }

        public static Vector3 Clamped01(this Vector3 vector)
        {
            return vector.Clamped(0f, 1f);
        }
    }
}