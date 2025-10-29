using Noise;
using UnityEngine;

namespace Swim
{
    public class PerlinNoiseSwimBehavior : ISwimBehavior
    {
        private const float NoiseScale = 0.1f;
        
        private readonly IPerlinNoiseProvider perlinNoiseProvider;
        
        private readonly float noiseOffsetX;
        private readonly float noiseOffsetY;

        public PerlinNoiseSwimBehavior(IPerlinNoiseProvider perlinNoiseProvider)
        {
            this.perlinNoiseProvider = perlinNoiseProvider;
            
            noiseOffsetX = Random.Range(0, 1000f);
            noiseOffsetY = Random.Range(0, 1000f);
        }
        
        public void Swim(Transform transform, float swimSpeed)
        {
            var timeScale = Time.time * NoiseScale;

            var noiseValueX = perlinNoiseProvider.Generate1D(timeScale + noiseOffsetX);
            var noiseValueY = perlinNoiseProvider.Generate1D(timeScale + noiseOffsetY);
            
            var x = Extensions.MathfExtensions.Map(noiseValueX, 0, 1, -1, 1);
            var y = Extensions.MathfExtensions.Map(noiseValueY, 0, 1, -1, 1);
            
            var velocity = new Vector3(x, y, 0).normalized * (Time.deltaTime * swimSpeed);

            transform.rotation = Quaternion.Euler(0, velocity.x >= 0 ? 0 : 180, 0);

            transform.Translate(velocity, Space.World);
        }
    }
}