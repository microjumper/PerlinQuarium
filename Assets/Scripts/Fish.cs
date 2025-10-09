using UnityEngine;

public class Fish : MonoBehaviour
{
    public Vector2 aquariumMin = new Vector2(-8f, -2f);
    public Vector2 aquariumMax = new Vector2(8f, 4f);
    
    public float turnSpeed = 2f; // How fast the fish can change direction
    public float noiseScale = 0.5f; // Controls the "wideness" of the turns (frequency)
    
    private float swimSpeed;
    private float noiseOffset;
    
    private Vector2 velocity;

    private void Start()
    {
        noiseOffset = Random.Range(0, 1000f); 
        swimSpeed = Random.Range(1f, 3f);
        
        velocity = Random.insideUnitCircle.normalized * swimSpeed;
    }

    private void Update()
    {
        var noiseValue = Mathf.PerlinNoise(Time.time * noiseScale, noiseOffset);
        
        // Map the noise (0 to 1) to a turning range [-1 to 1]
        var turnDirection = (noiseValue * 2f) - 1f; 
        
        var currentAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        
        currentAngle += turnDirection * turnSpeed * Time.deltaTime * 60;
        
        velocity.x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * swimSpeed;
        velocity.y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * swimSpeed;

        var nextPosition = transform.position + (Vector3)velocity * Time.deltaTime;
        
        if (nextPosition.x < aquariumMin.x || nextPosition.x > aquariumMax.x)
        {
            velocity.x *= -1f;
        }
        if (nextPosition.y < aquariumMin.y || nextPosition.y > aquariumMax.y)
        {
            velocity.y *= -1f;
        }
        
        transform.position = nextPosition;

        UpdateOrientation();
    }
    
    private void UpdateOrientation()
    {
        var direction = Mathf.Sign(velocity.x);
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z); 
    }
}