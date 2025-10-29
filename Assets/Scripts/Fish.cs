using UnityEngine;
using Movement;
using Noise;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private FishData fishData;
    
    private SpriteRenderer spriteRenderer;
    
    private FishTank fishTank;
    
    private IMovementProvider movementProvider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fishData.Sprite;
    }

    private void Start()
    {
        movementProvider = new PerlinNoiseMovementProvider(new UnityPerlinNoiseProvider());
    }

    private void Update()
    {
        Swim();
    }

    private void Swim()
    {
        var velocity = movementProvider.ProvideVelocityVector();
        var delta = velocity.normalized * (Time.deltaTime * fishData.SwimSpeed);
        
        var targetPosition = transform.position + delta;

        if (fishTank)
        {
            if (!fishTank.horizontalBoundary.Contains(targetPosition.x))
            {
                targetPosition.x = fishTank.horizontalBoundary.Clamp(targetPosition.x);
            }

            if (!fishTank.verticalBoundary.Contains(targetPosition.y))
            {
                targetPosition.y = fishTank.verticalBoundary.Clamp(targetPosition.y);
            }
        }

        transform.position = targetPosition;
        
        transform.rotation = Quaternion.Euler(0, delta.x >= 0 ? 0 : 180, 0);
    }

    public void SetFishData(FishData newFishData)
    {
        fishData = newFishData;
        
        spriteRenderer.sprite = fishData.Sprite;
    }
    
    public void SetFishTank(FishTank newFishTank) => fishTank = newFishTank;
}