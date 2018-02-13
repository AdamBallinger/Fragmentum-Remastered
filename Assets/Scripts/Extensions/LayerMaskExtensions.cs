using UnityEngine;

namespace Scripts.Extensions
{
    public static class LayerMaskExtensions
    {
        public static bool Contains(this LayerMask _self, int _layer)
        {
            return _self == (_self | (1 << _layer));
        }
    }
}
