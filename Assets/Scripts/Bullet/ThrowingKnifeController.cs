using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnifeController : MonoBehaviour
{
    private GameObject player;
    private float speed = 600f;
    private int damage = 2;

    // 구조 바꾸면 필요 없는것들
    private ShieldController shield;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;

        shield = player.GetComponent<ShieldController>(); // 나중에 구조 바꿀것
        rb = gameObject.GetComponent<Rigidbody2D>();

        Vector3 dir = player.transform.position - gameObject.transform.position;
        rb.velocity = dir.normalized * speed;

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
    }

    // 추상화 예정
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int amount;
            if (shield.GetShieldState())
            {
                amount = 0;
                Debug.Log("Shield");
                shield.ReSetShield();
                ShieldmanController.Instance.ChangeHealth(amount);
            }
            else
            {
                amount = damage * -1;
                Debug.Log("Non Shield" + "damage : " + amount);
                ShieldmanController.Instance.ChangeHealth(amount);
            }
            Destroy(gameObject);
        }
    }
}
