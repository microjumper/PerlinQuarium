namespace Noise.Perlin.Provider
{
    public interface IPerlinNoiseProvider
    {
        float Generate1D(float x);
        float Generate2D(float x, float y);
    }
}