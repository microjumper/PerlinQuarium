using UnityEngine;

namespace Noise.Generators
{
    public interface INoiseGenerator
    {
        Vector2 GenerateUnitVector2();
        Vector2 GeneratePositionWithinBoundaries(Boundary horizontalBoundary, Boundary verticalBoundary);
    }
}