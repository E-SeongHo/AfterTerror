using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int damage = 1;
    private Rigidbody2D rb; 
    private Transform playerTransform;

    private void Awake()
    {
        // player는 계속 같은 자리에 존재하므로 Awake에서 Transform 가져온다.
        playerTransform = GameObject.FindGameObjectWithTag("Shieldman").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Vector3 dir = playerTransform.position - transform.position;
        // bullet은 등속도 운동
        rb.velocity = dir.normalized * speed;

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Shieldman")
        {
            ShieldmanController shieldmanController = other.GetComponent<ShieldmanController>();
            shieldmanController.ChangeHealth(damage * -1);

            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }
        
}

// 
