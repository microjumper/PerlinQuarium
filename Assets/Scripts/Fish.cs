using UnityEngine;
using Extensions;
using Noise;

public class Fish : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private FishData fishData;

    [SerializeField]
    private float noiseScale = 0.1f;
    private float noiseOffset;
    
    private IPerlinNoiseProvider perlinNoiseProvider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteRenderer.sprite = fishData.Sprite;
        
        noiseOffset = Random.Range(0, 1000f);

        perlinNoiseProvider = new UnityPerlinNoiseProvider();
    }

    private void Update()
    {
        Swim();
    }

    private void Swim()
    {
        var targetPosition = SamplePosition();
        
        UpdateOrientation(targetPosition);
        
        transform.position = targetPosition;
    }

    private Vector3 SamplePosition()
    {
        var timeScale = Time.time * noiseScale;
        
        var noiseValueX = Mathf.PerlinNoise1D(timeScale);
        var noiseValueY = Mathf.PerlinNoise1D(timeScale + noiseOffset);
        
        var x = MathfExtensions.Map(noiseValueX, 0, 1, PlayArea2D.Instance.horizontalBoundary.Min,
            PlayArea2D.Instance.horizontalBoundary.Max);
        var y = MathfExtensions.Map(noiseValueY, 0, 1, PlayArea2D.Instance.verticalBoundary.Min,
            PlayArea2D.Instance.verticalBoundary.Max);
        
        return new Vector3(x, y, transform.position.z);
    }
    
    private void UpdateOrientation(Vector3 targetPosition)
    {
        var direction = Mathf.Sign(targetPosition.x - transform.position.x);
        var scale = transform.localScale;

        scale.x = direction;
        transform.localScale = scale;
    }
}