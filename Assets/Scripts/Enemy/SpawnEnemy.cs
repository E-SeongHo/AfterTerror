using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;
    public ObjectManager objectManager;
    float maxSpawnDelay = 2.0f;
    float curSpawnDelay;

    void Awake()
    {
        enemyObjs = new string[] { "Enemy" }; // 새로운 적 종류가 생기면 추가
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawningEnemy();
            curSpawnDelay = 0;
        }
    }

    void SpawningEnemy()
    {
        int ranEnemy = Random.Range(0, 3); // 몇 개 소환할 지
        int ranPoint = Random.Range(0, 5);
        GameObject enemy = objectManager.MakeObj(enemyObjs[ranEnemy]);
        enemy.transform.position = spawnPoints[ranPoint].position;
    }
}