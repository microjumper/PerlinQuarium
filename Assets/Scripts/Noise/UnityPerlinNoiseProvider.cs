using UnityEngine;

namespace Noise
{
    public class UnityPerlinNoiseProvider : IPerlinNoiseProvider
    {
        public float Generate2D(float x, float y) => Mathf.PerlinNoise(x, y);
    }
}