using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 position;
    public float moveSpeed = -1f;
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    void moveEnemy()
    {
        position.x += moveSpeed * Time.deltaTime;
        transform.position = position;
    }
}
