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
    
    public bool Contains(float value) => value >= min && value <= max;
}