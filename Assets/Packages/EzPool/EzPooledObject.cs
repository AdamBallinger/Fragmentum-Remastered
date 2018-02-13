using UnityEngine;
// ReSharper disable CheckNamespace

namespace EzPool
{
    [DisallowMultipleComponent]
    public class EzPooledObject : MonoBehaviour
    {
        /// <summary>
        /// Reference to the pool manager this object is apart of.
        /// </summary>
        [HideInInspector]
        public EzPoolManager poolManager = null;

        /// <summary>
        /// Tells the pool manager to reclaim this object.
        /// </summary>
        public void RePool()
        {
            poolManager?.PoolObject(gameObject);
        }
    }
}