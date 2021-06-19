using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Map Blcok Spawner
// 재사용 X 미리 만들어 놓기만 한다. 
// 넘어간 맵은 스택에 모아뒀다가 일정 이상 쌓이면 Destory
public class MapBlockSpawner : MonoBehaviour
{
    public static MapBlockSpawner Instance;

    // 맵 블록 종류
    [SerializeField] private GameObject[] frontMapBlocks; // 근경블럭
    [SerializeField] private GameObject[] middleMapBlocks; // 중경블럭
    
    // 맵 블록 풀
    private Queue<GameObject> frontPool = new Queue<GameObject>();
    private Queue<GameObject> middlePool = new Queue<GameObject>();

    private int poolSize = 5; // 처음 생성개수
    private int createBound = 3; // 부족할 때 임계
    private int createNum = 10;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            CreateFrontBlockInPool();
        }
        Debug.Log("FrontMapPool Init");
    }
    private void Update()
    {
        if (frontPool.Count <= createBound)
        {
            Debug.Log("맵새로생성");
            for (int i = 0; i < createNum; i++)
            {
                CreateFrontBlockInPool();
            }
        }
    }
    private void CreateFrontBlockInPool()
    {
        GameObject newFront = Instantiate(frontMapBlocks[Random.Range(0, frontMapBlocks.Length)]);
        newFront.SetActive(false);
        frontPool.Enqueue(newFront);
        Debug.Log(frontPool.Count);
    }
 
    public GameObject AllocateFrontBlock()
    {
        if(frontPool.Count <= 0)
        {
            CreateFrontBlockInPool();
        }
        GameObject allocate = frontPool.Dequeue();
        // SetActive(true)는 호출자가 호출
        return allocate;
    }
}