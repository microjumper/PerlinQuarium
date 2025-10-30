using UnityEngine;

namespace Extensions
{
    public static class MathfExtensions
    {
        /// <summary>
        /// Maps a value from one range to another, clamping the input value within the source range.
        /// </summary>
        public static float Map(float value, float sourceMin, float sourceMax, float targetMin, float targetMax)
        {
            // Clamp the value to the source range
            value = Mathf.Clamp(value, sourceMin, sourceMax);
            
            // Perform linear mapping
            return (value - sourceMin) / (sourceMax - sourceMin) * (targetMax - targetMin) + targetMin;
        }
    }
}