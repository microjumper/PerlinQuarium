using UnityEngine;

namespace Extensions
{
    public static class MathfExtensions
    {
        /// <summary>
        /// Maps a value from one range to another, clamping the input value within the source range.
        /// </summary>
        public static float Map(float value, float sourceLowerBound, float sourceUpperBound, float targetLowerBound, float targetUpperBound)
        {
            // Clamp the value to the source range
            value = Mathf.Clamp(value, sourceLowerBound, sourceUpperBound);
            
            // Perform linear mapping
            return (value - sourceLowerBound) / (sourceUpperBound - sourceLowerBound) * (targetUpperBound - targetLowerBound) + targetLowerBound;
        }
    }
}