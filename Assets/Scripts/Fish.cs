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
        SwimWithinBoundaries();
    }

    private void SwimWithinBoundaries()
    {
        var targetPosition = noiseGenerator.GeneratePositionWithinBoundaries(fishTank.HorizontalBoundary, fishTank.VerticalBoundary);
        
        var delta = targetPosition.x - transform.position.x;
        
        if (Mathf.Abs(delta) > 0.001f)
        {
            transform.localScale = new Vector3(Mathf.Sign(delta), 1f, 1f);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * fishData.SwimSpeed);
    }

    public void SetFishData(FishData newFishData)
    {
        fishData = newFishData;
        
        spriteRenderer.sprite = fishData.Sprite;
    }
    
    public void SetFishTank(FishTank newFishTank) => fishTank = newFishTank;
}