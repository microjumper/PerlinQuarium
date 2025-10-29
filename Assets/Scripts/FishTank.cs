using System;
using UnityEngine;

public class FishTank : MonoBehaviour
{
    public Boundary horizontalBoundary = new (-9, 9);
    public Boundary verticalBoundary = new (-5, 5);
    
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