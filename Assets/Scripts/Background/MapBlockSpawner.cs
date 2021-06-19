using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Map Blcok Spawner
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

    private int poolSize = 5; // ó�� ��������
    private int createBound = 3; // ������ �� �Ӱ�
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
            Debug.Log("�ʻ��λ���");
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
        // SetActive(true)�� ȣ���ڰ� ȣ��
        return allocate;
    }
}