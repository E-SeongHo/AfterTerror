using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteController : MonoBehaviour
{
    private float duration = 2f;
    private int damage = 2;

    private GameObject player; // 목표

    [Range(0, 1)] private float bezier_param = 0; // Bezier Curve 매개변수
    private float height = 500f; // Bezier Point 최대 상대높이
    private float startTime;

    private Rigidbody2D rb;
    private Animator animator;
    private ShieldController shield; 

    Vector2[] points = new Vector2[4];

    private void Start()
    {
        startTime = Time.time;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        shield = player.GetComponent<ShieldController>();

        // setting bezier points
        points[0] = gameObject.transform.position + new Vector3(-50f, 0, 0);
        points[1] = gameObject.transform.position + new Vector3(-500f, height, 0);
        points[2] = player.transform.position + new Vector3(200f, height, 0);
        points[3] = player.transform.position; 
    }

    private void FixedUpdate()
    {
        if (bezier_param > 1) return;
        bezier_param = (Time.time - startTime) / duration;
        float angle = bezier_param * 180;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        DrawTrajectory();
    }
    private void DrawTrajectory() // no consider rotation
    {
        transform.position = new Vector2(
        BezierCurve3D(points[0].x, points[1].x, points[2].x, points[3].x),
        BezierCurve3D(points[0].y, points[1].y, points[2].y, points[3].y)
        );
    }
    private float BezierCurve3D(float pp1, float pp2, float pp3, float pp4)
    {
        return Mathf.Pow((1 - bezier_param), 3) * pp1
            + Mathf.Pow((1 - bezier_param), 2) * 3 * bezier_param * pp2
            + Mathf.Pow(bezier_param, 2) * 3 * (1 - bezier_param) * pp3
            + Mathf.Pow(bezier_param, 3) * pp4;
    }
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
            }
            else
            {
                amount = damage * -1;
                Debug.Log("Non Shield" + "damage : " + amount);
                ShieldmanController.Instance.ChangeHealth(amount);
            }
            animator.SetBool("arrive", true);
            Destroy(gameObject, 0.5f);
        }
    }

}
