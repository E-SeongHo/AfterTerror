using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float enemySpeed = 200f;
    public bool isPause = false;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        //Enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Debug.Log(transform.position);
        transform.Translate(-1 * enemySpeed * Time.deltaTime, 0, 0);
    }
}
