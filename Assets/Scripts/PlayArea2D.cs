using System;
using UnityEngine;

public class PlayArea2D : MonoBehaviour
{
    public static PlayArea2D Instance { get; private set; }
    
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;
    
    private void Awake()
    {
        if (Instance&& Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }
        
        ValidateBoundaries();
    }
    
    private void ValidateBoundaries()
    {
        if (horizontalBoundary.Min >= horizontalBoundary.Max || verticalBoundary.Min >= verticalBoundary.Max)
        {
            throw new ArgumentException("Invalid boundaries");
        }
    }
    
#if UNITY_EDITOR
    [Space]
    [SerializeField]
    private bool boundariesVisible = true;
    
    private Vector3[] boundaryPoints;
    
    private void OnValidate()
    {
        ValidateBoundaries();
        
        boundaryPoints = new Vector3[4];
        
        boundaryPoints[0] = new Vector3(horizontalBoundary.Min, verticalBoundary.Min, 0f);
        boundaryPoints[1] = new Vector3(horizontalBoundary.Max, verticalBoundary.Min, 0f);
        boundaryPoints[2] = new Vector3(horizontalBoundary.Max, verticalBoundary.Max, 0f);
        boundaryPoints[3] = new Vector3(horizontalBoundary.Min, verticalBoundary.Max, 0f);
    }
    
    private void OnDrawGizmos()
    {
        if (boundariesVisible)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLineStrip(boundaryPoints, true);
        }
    }
#endif
}