using UnityEngine;
using Noise;
using Random = UnityEngine.Random;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private FishData fishData;
    
    private SpriteRenderer spriteRenderer;
    
    public float noiseScale = 0.5f; // Controls the "wideness" of the turns (frequency)

    private float noiseOffset;

    private Vector2 velocity;

    private IPerlinNoiseProvider perlinNoiseProvider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = fishData.Sprite;
        
        noiseOffset = Random.Range(0, 1000f);

        velocity = Random.insideUnitCircle.normalized * fishData.SwimSpeed;

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

    private void SteerTowards(float steerDirection)
    {
        // How much to rotate this frame
        var targetAngle = steerDirection * fishData.TurnSpeed * Time.deltaTime;

        // Create a rotation around Z axis
        var rotation = Quaternion.Euler(0, 0, targetAngle);

        // Rotate the velocity vector
        velocity = rotation * velocity;

        // Keep speed constant
        velocity = velocity.normalized * fishData.SwimSpeed;
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

    private void UpdateOrientation()
    {
        var direction = Mathf.Sign(velocity.x);
        var scale = transform.localScale;

        scale.x = direction;
        transform.localScale = scale;
    }
}