using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Map Block Spawner
// ���� X �̸� ����� ���⸸ �Ѵ�. 
// �Ѿ ���� ���ÿ� ��Ƶ״ٰ� ���� �̻� ���̸� Destory
public class MapBlockSpawner : MonoBehaviour
{
    public static MapBlockSpawner Instance;

    // �� ��� ����
    [SerializeField] private GameObject[] frontMapBlocks; // �ٰ��
    [SerializeField] private GameObject[] middleMapBlocks; // �߰��
    
    // �� ��� Ǯ
    private Queue<GameObject> frontPool = new Queue<GameObject>();
    private Queue<GameObject> middlePool = new Queue<GameObject>();

    // front ����.. middle�� ����
    private int poolSize = 10; // ó�� ��������
    private int createBound = 6; // ������ �� �Ӱ�
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
        // SetActive(true)�� ȣ���ڰ� ȣ��� ��ǥ����
        return allocate;
    }
    public GameObject AllocateMiddleBlock()
    {
        if(middlePool.Count <= 0)
        {
            CreateMiddleBlockInPool();
        }
        GameObject allocate = middlePool.Dequeue();
        // SetActive(true)�� ȣ���ڰ� ȣ��� ��ǥ����
        return allocate;
    }
}