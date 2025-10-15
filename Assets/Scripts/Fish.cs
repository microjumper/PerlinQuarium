using Noise;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float turnSpeed = 2f; // How fast the fish can change direction
    public float noiseScale = 0.5f; // Controls the "wideness" of the turns (frequency)

    private float swimSpeed;
    private float noiseOffset;

    private Vector2 velocity;

    private IPerlinNoiseProvider perlinNoiseProvider;

    private void Start()
    {
        noiseOffset = Random.Range(0, 1000f);
        swimSpeed = Random.Range(1f, 3f);

        velocity = Random.insideUnitCircle.normalized * swimSpeed;
        
        perlinNoiseProvider = new RustPerlinNoiseProvider();
    }

    private void Update()
    {
        Swim();
    }

    private void Swim()
    {
        var steerDirection = SampleSteeringDirection();

        SteerTowards(steerDirection);

        var targetPosition = transform.position + (Vector3)velocity * Time.deltaTime;

        SetPosition(targetPosition);

        UpdateOrientation();
    }

    private float SampleSteeringDirection()
    {
        var noiseValue = perlinNoiseProvider.Generate2D(Time.time * noiseScale, noiseOffset);

        // Map the noise (0 to 1) to a turning range [-1 to 1]
        var steerDirection = (noiseValue * 2f) - 1f;

        return steerDirection;
    }

    private void SetPosition(Vector3 targetPosition)
    {
        if (targetPosition.x < Tank.Instance.horizontalBoundary.min ||
            targetPosition.x > Tank.Instance.horizontalBoundary.max)
        {
            velocity.x *= -1f;
            targetPosition.x = Mathf.Clamp(targetPosition.x, Tank.Instance.horizontalBoundary.min,
                Tank.Instance.horizontalBoundary.max);
        }

        if (targetPosition.y < Tank.Instance.verticalBoundary.min ||
            targetPosition.y > Tank.Instance.verticalBoundary.max)
        {
            velocity.y *= -1f;
            targetPosition.y = Mathf.Clamp(targetPosition.y, Tank.Instance.verticalBoundary.min,
                Tank.Instance.verticalBoundary.max);
        }

        transform.position = targetPosition;
    }

    private void SteerTowards(float steerDirection)
    {
        // How much to rotate this frame
        var targetAngle = steerDirection * turnSpeed * Time.deltaTime;

        // Create a rotation around Z axis
        var rotation = Quaternion.Euler(0, 0, targetAngle);

        // Rotate the velocity vector
        velocity = rotation * velocity;

        // Keep speed constant
        velocity = velocity.normalized * swimSpeed;
    }

    private void UpdateOrientation()
    {
        var direction = Mathf.Sign(velocity.x);
        var scale = transform.localScale;

        scale.x = direction;
        transform.localScale = scale;
    }
}