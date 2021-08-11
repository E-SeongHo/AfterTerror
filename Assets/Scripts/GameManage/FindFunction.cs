using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFunction : MonoBehaviour
{
    static FindFunction instance;
    public static FindFunction Instance
    {
        get {return instance;}
    }
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        instance = this;
    }
    public GameObject FindNearestObject(List<GameObject> tagged)
    {
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity; 
        Vector2 currentPos= player.GetComponent<Transform>().position;
        foreach(GameObject target in tagged)
        {
            Vector2 targetPos = target.GetComponent<Transform>().position;
            float distance = Vector2.Distance(targetPos, currentPos);
            if(distance < minDistance)
            {
                minDistance = distance; // 거리값 필요시 사용
                nearestTarget = target;
            } 
        }
        return nearestTarget;
    }
    public GameObject FindNearestObjectArr(GameObject[] tagged)
    {
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPos = player.GetComponent<Transform>().position;
        foreach (GameObject target in tagged)
        {
            Vector2 targetPos = target.GetComponent<Transform>().position;
            float distance = Vector2.Distance(targetPos, currentPos);
            if (distance < minDistance)
            {
                minDistance = distance; // 거리값 필요시 사용
                nearestTarget = target;
            }
        }
        return nearestTarget;
    }
    public GameObject FindNearestObjectArrWithX(GameObject[] tagged)
    {
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPos = player.GetComponent<Transform>().position;
        foreach (GameObject target in tagged)
        {
            Vector2 targetPos = target.GetComponent<Transform>().position;
            float distance = targetPos.x - currentPos.x;
            if (distance < minDistance)
            {
                minDistance = distance; // 거리값 필요시 사용
                nearestTarget = target;
            }
        }
        return nearestTarget;
    }
    // interaction = true인 적 중 가장 가까운 적 (x좌표) 계산 
    public GameObject FindNearestInteractionObjectArrWithX(GameObject[] tagged)
    {
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPos = player.GetComponent<Transform>().position;
        foreach (GameObject target in tagged)
        {
            // interaction = true인 적만 대상
            if (!target.GetComponent<EnemyController>().GetInteractionState()) continue;
            Vector2 targetPos = target.GetComponent<Transform>().position;
            float distance = targetPos.x - currentPos.x;
            if (distance < minDistance)
            {
                minDistance = distance; // 거리값 필요시 사용
                nearestTarget = target;
            }
        }
        return nearestTarget;
    }
    public List<GameObject> GetEnemiesInSameBlock(Transform block)
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach(Transform child in block)
        {
            if (child.tag == "Enemy")
                enemies.Add(child.gameObject);
        }
        return enemies;
    }
}
