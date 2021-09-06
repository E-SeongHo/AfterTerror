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
        // player�� ��� ���� �ڸ��� �����ϹǷ� Awake���� Transform �����´�.
        playerTransform = ShieldmanController.Instance.transform;
        // ??? ȣ�� �ʹ� ����...
        shield = ShieldmanController.Instance.gameObject.GetComponent<ShieldController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        
        Vector3 dir = playerTransform.position - transform.position;
        // bullet�� ��ӵ� �
        rb.velocity = dir.normalized * speed;

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }
 
    // Player�� ȣ���ϵ��� �ϴ� ���� ���� �� ����
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
