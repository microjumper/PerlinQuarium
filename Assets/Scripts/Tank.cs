using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float min = -10f;
    public float max = 10f;
}

public class Tank : MonoBehaviour
{
    public static Tank Instance { get; private set; }
    
    public Boundary horizontalBoundary;
    public Boundary verticalBoundary;
    
    [Space]
    [SerializeField]
    private bool boundariesVisible = true;
    
    private Vector3[] boundaryPoints;
    
    private void Awake()
    {
        if (Instance && Instance != this)
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
        
        boundaryPoints[0] = new Vector3(horizontalBoundary.min, verticalBoundary.min, 0f);
        boundaryPoints[1] = new Vector3(horizontalBoundary.max, verticalBoundary.min, 0f);
        boundaryPoints[2] = new Vector3(horizontalBoundary.max, verticalBoundary.max, 0f);
        boundaryPoints[3] = new Vector3(horizontalBoundary.min, verticalBoundary.max, 0f);
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