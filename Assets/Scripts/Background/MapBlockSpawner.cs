using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Map Block Spawner
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

    // front 기준.. middle은 절반
    private int poolSize = 10; // 처음 생성개수
    private int createBound = 6; // 부족할 때 임계
    private int createNum = 10;

    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < poolSize; i++)
        {
            CreateFrontBlockInPool();
        }
        for (int i = 0; i < poolSize/2; i++)
        {
            CreateMiddleBlockInPool();   
        }
    }
    private void Update()
    {
        if (frontPool.Count <= createBound)
        {
            for (int i = 0; i < createNum; i++)
            {
                CreateFrontBlockInPool();
            }
        }
        if (middlePool.Count <= createBound/2)
        {
            for(int i = 0; i < createNum/2; i++)
            {
                CreateMiddleBlockInPool();
            }
        }
    }
    private void CreateFrontBlockInPool()
    {
        GameObject newFront = Instantiate(frontMapBlocks[Random.Range(0, frontMapBlocks.Length)]);
        newFront.SetActive(false);
        frontPool.Enqueue(newFront);
    }
    private void CreateMiddleBlockInPool()
    {
        GameObject newMiddle = Instantiate(middleMapBlocks[Random.Range(0, middleMapBlocks.Length)]);
        newMiddle.SetActive(false);
        middlePool.Enqueue(newMiddle);
    }
    public GameObject AllocateFrontBlock()
    {
        if(frontPool.Count <= 0)
        {
            CreateFrontBlockInPool();
        }
        GameObject allocate = frontPool.Dequeue();
        // SetActive(true)는 호출자가 호출및 좌표설정
        return allocate;
    }
    public GameObject AllocateMiddleBlock()
    {
        if(middlePool.Count <= 0)
        {
            CreateMiddleBlockInPool();
        }
        GameObject allocate = middlePool.Dequeue();
        // SetActive(true)는 호출자가 호출및 좌표설정
        return allocate;
    }
}