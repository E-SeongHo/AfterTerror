using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //private GameObject enemy;
    [SerializeField] private float bulletSpeed = 2f;
    public GameObject bullet;
    public Transform pos;
    public int damage = 1;
    public Rigidbody2D rb;
    public float distance;
    public LayerMask isLayer;

    EnemyButton enemyButton = GameObject.Find("soldier").GetComponent<EnemyButton>();
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        // asasfa
        rb.velocity = transform.right * bulletSpeed;
    }
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if (ray.collider != null)
        {
            if (ray.collider.tag == "shieldman")
            {
                Debug.Log("shieldman hitted");
            }
            gameObject.SetActive(false);
        }
        Vector2 position = rb.position;
        rb.MovePosition(position);
        position.x = position.x - bulletSpeed * Time.deltaTime;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        ShieldmanController shieldmanController = other.GetComponent<ShieldmanController>();
        if (other != null)
        {
            shieldmanController.ChangeHealth(damage * -1);
        }
        // 총알 피하는 버튼 구현
        gameObject.SetActive(false);
    }
}