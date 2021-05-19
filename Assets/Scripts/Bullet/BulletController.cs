using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //private GameObject enemy;
    [SerializeField] private float speed = 2f;
    public int damage = 1;
    public Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        // asasfa
        rb.velocity = transform.right * speed ;
    }
    void Update()
    {
        Vector2 position = rb.position;
        position.x = position.x - speed * Time.deltaTime;
        rb.MovePosition(position);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Shieldman")
        {
            ShieldmanController shieldmanController = other.GetComponent<ShieldmanController>();
            if (other != null)
            {
                shieldmanController.ChangeHealth(damage * -1);
            }
            // 총알 피하는 버튼 구현
            Destroy(gameObject);
        }
    }
        
}
