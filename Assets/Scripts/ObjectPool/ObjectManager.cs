using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject playerBullet1Prefab;
    public GameObject enemyBullet1Prefab;
    // public GameObject obstaclePrefab; // 지뢰, 구덩이, 기타등등 -> 몇개 생기는지 알려주세여... ( 아직 몇 개인지 확실하게 정해지지 않았으므로 추가사항 생기면 추가할 것)
    GameObject[] enemy1;
    // enemy 종류가 아직은 하나라서 일단 1로 분류해놨음. 나중에 더 생기면 추가할 것.
    // GameObject[] playerBullet1; // 즉발이라서 playerbullet1은 그냥 싹 없앨것

    GameObject[] enemyBullet1;

    // 적마다 공격 방식이 다름. -> 아이디어 같이 볼거니까 이 후에 추가.

    GameObject[] targetPool;
    void Awake()
    {
        enemy1 = new GameObject[10];
        enemyBullet1 = new GameObject[100]; // 이것도 모름. 더 나올 수도 있음.
        // playerBullet1 = new GameObject[50];

        Generate();
    }

    void Generate()
    {
        // #1. enemy
        for (int index = 0; index < enemy1.Length; index++)
        {
            enemy1[index] = Instantiate(enemy1Prefab);
            enemy1[index].SetActive(false);
        }

        // #2. bullet
        for (int index = 0; index < enemyBullet1.Length; index++)
        {
            enemyBullet1[index] = Instantiate(enemyBullet1Prefab);
            enemyBullet1[index].SetActive(false);
        }
        // for (int index = 0; index < playerBullet1.Length; index++)
        // {
        //     playerBullet1[index] = Instantiate(playerBullet1Prefab);
        //     playerBullet1[index].SetActive(false);
        // }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "enemy1":
                targetPool = enemy1;
                break;

            case "enemyBullet1":
                targetPool = enemyBullet1;
                break;

                // case "playBullet1":
                //     targetPool = playerBullet1;
                //     break;
        }
        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
                targetPool[index].SetActive(true);
            return targetPool[index];
        }
        return null;
    }
}

