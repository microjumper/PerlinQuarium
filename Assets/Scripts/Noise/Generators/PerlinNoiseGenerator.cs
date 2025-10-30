using UnityEngine;
using Extensions;
using Noise.Perlin.Providers;

namespace Noise.Generators
{
    public class PerlinNoiseGenerator : INoiseGenerator
    {
        private const float TimeScale = 0.1f;
        
        private readonly IPerlinNoiseProvider perlinNoiseProvider;
        
        private readonly float noiseOffsetX;
        private readonly float noiseOffsetY;
        
        public PerlinNoiseGenerator(IPerlinNoiseProvider perlinNoiseProvider)
        {
            this.perlinNoiseProvider = perlinNoiseProvider;
            
            noiseOffsetX = Random.Range(0, 1000f);
            noiseOffsetY = Random.Range(0, 1000f);
        }

        public Vector2 GenerateVector2()
        {
            var t = Time.time * TimeScale;
            
            var noiseValueX = perlinNoiseProvider.Generate2D(t + noiseOffsetX, noiseOffsetY);
            var noiseValueY = perlinNoiseProvider.Generate2D(noiseOffsetX, t + noiseOffsetY);
            
            var x = MathfExtensions.Map(noiseValueX, 0, 1, -1, 1);
            var y = MathfExtensions.Map(noiseValueY, 0, 1, -1, 1);
            
            return new Vector2(x, y);
        }
    }
}