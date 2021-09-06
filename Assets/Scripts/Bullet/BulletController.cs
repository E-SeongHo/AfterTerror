using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class BulletController : MonoBehaviour
{
    private float speed = 600f;
    private int damage = 1;
    private Rigidbody2D rb; 
    private Transform playerTransform;
    private Vector2 alter = new Vector2(0, -50f);
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
 
    // Player가 호출하도록 하는 것이 나을 것 같음
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            int amount;
            if (shield.GetShieldState())
            {
                amount = 0;
                Debug.Log("Shield");
                shield.ReSetShield();
            }
            else
            {
                amount = damage * -1;
                Debug.Log("Non Shield" + "damage : " + amount);
                ShieldmanController.Instance.ChangeHealth(amount);
            }
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }
        
}
