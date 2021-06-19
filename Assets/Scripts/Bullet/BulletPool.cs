using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private GameObject bulletPrefab;
    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    private int poolSize = 10;
    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            CreateBulletInPool();
        }
        Debug.Log("BulletPool Init");
    }

    private void CreateBulletInPool()
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bulletPool.Enqueue(newBullet);
    }

    public GameObject AllocateBullet()
    {
        if(bulletPool.Count <= 0)
        {
            CreateBulletInPool();
        }
        GameObject allocate = bulletPool.Dequeue();
        //allocate.SetActive(true);
        return allocate;
    }
    
    public void ReturnBullet(GameObject usedBullet)
    {
        usedBullet.SetActive(false);
        bulletPool.Enqueue(usedBullet);
    }
}
