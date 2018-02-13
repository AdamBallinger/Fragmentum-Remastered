using UnityEngine;

namespace Scripts.Extensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns the direction vector to the given vector target.
        /// </summary>
        /// <param name="_self"></param>
        /// <param name="_target"></param>
        /// <returns></returns>
        public static Vector3 DirectionTo(this Vector3 _self, Vector3 _target)
        {
            return _target - _self;
        }
    }
}
