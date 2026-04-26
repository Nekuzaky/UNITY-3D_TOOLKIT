using UnityEngine;

namespace GameJamToolkit.Utils
{
    public static class LayerMaskHelper
    {
        public static bool Contains(LayerMask mask, int layer) => ((1 << layer) & mask.value) != 0;

        public static int OnlyMask(int layer) => 1 << layer;
    }
}
