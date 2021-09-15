using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    /// ----- Enemy Life Cycle ----- 
    /// Create(with MapBlock) and SetActive(False) 
    /// SetActive(True) when d < 1920(1 block size)
    /// interaction = true when close to player in EnemyController script
    /// run = true and delete or destroyed by player's attack

    public static EnemyPool Instance;
    private List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        Debug.Log("asd");
        Instance = this;
    }
    public int GetPoolSize() { return enemies.Count; }
    public void PushEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    public GameObject GetNearestEnemy()
    {
        GameObject nearest_enemy;
        nearest_enemy = FindFunction.Instance.FindNearestObject(enemies);

        return nearest_enemy;
    }
    public void DeleteEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

}
