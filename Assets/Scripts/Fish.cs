using UnityEngine;
using Noise.Generators;
using Noise.Perlin.Providers;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private FishData fishData;
    
    private SpriteRenderer spriteRenderer;
    
    private FishTank fishTank;
    
    private INoiseGenerator noiseGenerator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fishData.Sprite;
    }

    private void Start()
    {
        noiseGenerator = new PerlinNoiseGenerator(new UnityPerlinNoiseProvider());
    }

    private void Update()
    {
        Swim();
    }

    private void Swim()
    {
        var velocity= noiseGenerator.GenerateVector2();
        
        var delta = (Vector3)velocity.normalized * (Time.deltaTime * fishData.SwimSpeed);
        
        var targetPosition = transform.position + delta;

        if (!fishTank.HorizontalBoundary.Contains(targetPosition.x))
        {
            targetPosition.x = fishTank.HorizontalBoundary.Clamp(targetPosition.x);
        }

        if (!fishTank.VerticalBoundary.Contains(targetPosition.y))
        {
            targetPosition.y = fishTank.VerticalBoundary.Clamp(targetPosition.y);
        }
        
        transform.position = targetPosition;

        if (velocity.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, delta.x >= 0 ? 0 : 180, 0);
        }
    }

    public void SetFishData(FishData newFishData)
    {
        fishData = newFishData;
        
        spriteRenderer.sprite = fishData.Sprite;
    }
    
    public void SetFishTank(FishTank newFishTank) => fishTank = newFishTank;
}