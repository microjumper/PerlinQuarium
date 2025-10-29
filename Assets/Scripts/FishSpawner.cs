using UnityEngine;
using UnityEngine.Pool;

public class FishSpawner : MonoBehaviour
{
    [SerializeField]
    private FishTank fishTank;
    
    [SerializeField]
    private int poolCapacity = 10;
    
    [SerializeField]
    private int poolMaxSize = 25;
    
    [SerializeField]
    private GameObject fishPrefab;
    
    [SerializeField]
    private FishData[] fishData;
    
    private IObjectPool<GameObject> pool;
    private GameObject poolContainer;
    
    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: CreateFish,
            actionOnGet: OnGetFish,
            actionOnRelease: OnReleaseFish,
            actionOnDestroy: OnDestroyFish,
            collectionCheck: true,
            defaultCapacity: poolCapacity,
            maxSize: poolMaxSize
            );
    }
    
    private void Start()
    {
        poolContainer = new GameObject($"{fishPrefab.name}Pool");

        InitPool();
    }
    
    private GameObject CreateFish()
    {
        var fishObject = Instantiate(fishPrefab, poolContainer.transform, true);
        fishObject.SetActive(false);
        
        var fish = fishObject.GetComponent<Fish>();
        
        var fishDataIndex = Random.Range(0, fishData.Length);
        fish.SetFishData(fishData[fishDataIndex]);
        
        fish.SetFishTank(fishTank);
        
        var randomPosition = GenerateRandomPositionWithinBoundaries();
        fishObject.transform.position = randomPosition;
        
        return fishObject;
    }

    private void OnGetFish(GameObject fishObject) => fishObject.SetActive(true);

    private void OnReleaseFish(GameObject fishObject) => fishObject.SetActive(false);

    private void OnDestroyFish(GameObject fishObject) => Destroy(fishObject);

    private void InitPool()
    {
        for(var i = 0; i < poolCapacity; i++)
        {
            pool.Get();
        }
    }
    
    private Vector3 GenerateRandomPositionWithinBoundaries()
    {
        var x = Random.Range(fishTank.horizontalBoundary.Min, fishTank.horizontalBoundary.Max);
        var y = Random.Range(fishTank.verticalBoundary.Min, fishTank.verticalBoundary.Max);
        
        return new Vector3(x, y, 0f);
    }
}
