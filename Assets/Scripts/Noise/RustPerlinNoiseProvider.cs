using System.Runtime.InteropServices;

namespace Noise
{
    public class RustPerlinNoiseProvider : IPerlinNoiseProvider
    {
        [DllImport("simple_noise")]
        private static extern float perlin_noise_2d(float x, float y);
        
        public float Generate2D(float x, float y) => perlin_noise_2d(x, y);
        
        public float Generate1D(float x) => 0; // TODO
    }
}