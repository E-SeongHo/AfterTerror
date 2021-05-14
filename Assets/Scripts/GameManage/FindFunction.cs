using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindFunction : MonoBehaviour
{
    static FindFunction instance;
    public static FindFunction Instance
    {
        get { return instance; }
    }
    GameObject mainCharacter;

    private void Awake()
    {
        mainCharacter = GameObject.FindWithTag("Shieldman");
        instance = this;
    }
    public GameObject FindNearestObject(List<GameObject> tagged)
    {
        GameObject nearestTarget = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPos = mainCharacter.GetComponent<Transform>().position;
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
}
