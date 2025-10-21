using UnityEngine;

[System.Serializable]
public struct Boundary
{
    [SerializeField]
    private float min;
    [SerializeField]
    private float max;
    
    public float Min => min;
    public float Max => max;
    
    public Boundary(float min, float max)
    {
        if (min < max)
        {
            this.min = min;
            this.max = max;
        }
        else
        {
            throw new System.ArgumentException("Min value must be less than max value.");
        }
    }
    
    public bool Contains(float value) => value >= min && value <= max;
}