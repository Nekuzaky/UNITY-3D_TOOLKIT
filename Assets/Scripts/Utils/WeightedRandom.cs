using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Utils
{
    /// <summary>Weighted random pick. Ordered list, sum recomputed every call.</summary>
    public static class WeightedRandom
    {
        public static int PickIndex(IList<float> weights)
        {
            Debug.Assert(weights != null && weights.Count > 0, "[WeightedRandom.PickIndex] weights is empty"); // R5
            if (weights == null || weights.Count == 0) return -1;
            float total = 0f;
            int max = weights.Count; // R2
            for (int i = 0; i < max; i++)
            {
                if (weights[i] > 0f) total += weights[i];
            }
            if (total <= 0f) return 0;
            float r = Random.Range(0f, total);
            float acc = 0f;
            for (int i = 0; i < max; i++)
            {
                acc += Mathf.Max(0f, weights[i]);
                if (r <= acc) return i;
            }
            return max - 1;
        }
    }
}
