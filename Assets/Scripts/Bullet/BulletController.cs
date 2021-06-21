using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class BulletController : MonoBehaviour
{
    private float speed = 600f;
    [SerializeField] private int damage = 1;
    private Rigidbody2D rb; 
    private Transform playerTransform;
    private ShieldController shield;

    private void Awake()
    {
        // player는 계속 같은 자리에 존재하므로 Awake에서 Transform 가져온다.
        playerTransform = ShieldmanController.Instance.transform;
        // ??? 호출 너무 복잡...
        shield = ShieldmanController.Instance.gameObject.GetComponent<ShieldController>();
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
        if(other.tag == "Player")
        {
            int amount = damage;
            if (shield.GetShieldState())
            {
                damage = 0;
                Debug.Log("Shield");
            }
            ShieldmanController.Instance.ChangeHealth(damage * -1);
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }
        
}
