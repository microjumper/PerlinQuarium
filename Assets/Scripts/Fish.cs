using UnityEngine;
using Noise;
using Swim;

public class Fish : MonoBehaviour
{
    [SerializeField]
    private FishData fishData;
    
    private SpriteRenderer spriteRenderer;
    
    private ISwimBehavior swimBehavior;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = fishData.Sprite;

        swimBehavior = new PerlinNoiseSwimBehavior(new UnityPerlinNoiseProvider());
    }

    private void Update()
    {
        swimBehavior.Swim(transform, fishData.SwimSpeed);
    }

    public void SetFishData(FishData newFishData)
    {
        fishData = newFishData;
        
        spriteRenderer.sprite = fishData.Sprite;
    }
}