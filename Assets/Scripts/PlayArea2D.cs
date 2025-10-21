using UnityEngine;

public class PlayArea2D : MonoBehaviour
{
    public static PlayArea2D Instance { get; private set; }
    
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;
    
    [Space]
    [SerializeField]
    private bool boundariesVisible = true;
    
    private Vector3[] boundaryPoints;
    
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
    }
    
    private void OnValidate()
    {
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
}