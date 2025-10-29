using Noise;
using Noise.Perlin.Provider;
using UnityEngine;

namespace Movement
{
    public class PerlinNoiseMovementProvider : IMovementProvider
    {
        private const float NoiseScale = 0.1f;
        
        private readonly IPerlinNoiseProvider perlinNoiseProvider;
        
        private readonly float noiseOffsetX;
        private readonly float noiseOffsetY;

        public PerlinNoiseMovementProvider(IPerlinNoiseProvider perlinNoiseProvider)
        {
            this.perlinNoiseProvider = perlinNoiseProvider;
            
            noiseOffsetX = Random.Range(0, 1000f);
            noiseOffsetY = Random.Range(0, 1000f);
        }
        
        public Vector3 ProvideVelocityVector()
        {
            var timeScale = Time.time * NoiseScale;

            var noiseValueX = perlinNoiseProvider.Generate1D(timeScale + noiseOffsetX);
            var noiseValueY = perlinNoiseProvider.Generate1D(timeScale + noiseOffsetY);
            
            var x = Extensions.MathfExtensions.Map(noiseValueX, 0, 1, -1, 1);
            var y = Extensions.MathfExtensions.Map(noiseValueY, 0, 1, -1, 1);
            
            var velocity = new Vector3(x, y, 0);
            
            return velocity;
        }
    }
}