using UnityEngine;

namespace Noise.Perlin.Providers
{
    public class UnityPerlinNoiseProvider : IPerlinNoiseProvider
    {
        public float Generate1D(float x) => Mathf.Clamp(Mathf.PerlinNoise1D(x), 0, 1);
        public float Generate2D(float x, float y) => Mathf.Clamp(Mathf.PerlinNoise(x, y), 0, 1);
    }
}