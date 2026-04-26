using UnityEngine;
using URand = UnityEngine.Random;

namespace GameJamToolkit.Utils
{
    /// <summary>Seedable RNG usable outside the global gameplay random.</summary>
    public sealed class RandomGenerator
    {
        private URand.State _state;

        public RandomGenerator(int seed)
        {
            URand.InitState(seed);
            _state = URand.state;
        }

        public float Range(float min, float max)
        {
            URand.state = _state;
            float r = URand.Range(min, max);
            _state = URand.state;
            return r;
        }

        public int RangeInt(int minInclusive, int maxExclusive)
        {
            URand.state = _state;
            int r = URand.Range(minInclusive, maxExclusive);
            _state = URand.state;
            return r;
        }

        public Vector3 InsideUnitSphere()
        {
            URand.state = _state;
            Vector3 v = URand.insideUnitSphere;
            _state = URand.state;
            return v;
        }
    }
}
