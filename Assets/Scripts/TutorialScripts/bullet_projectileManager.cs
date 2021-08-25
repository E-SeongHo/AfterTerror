using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_projectileManager : MonoBehaviour
{
    [SerializeField] GameObject ShieldMan;
    [SerializeField] GameObject AttackEnemy;
    public GameObject bullet;
    private float bulletSpeed = 600f;
    Vector3 posSelf;

    private void Start()
    {

        bullet.SetActive(false);
    }
    private void Update()
    {
        if (AttackEnemy.activeSelf == true)
        {
            bullet.SetActive(true);
        }
        float moveX = -1 * bulletSpeed * Time.deltaTime;
        transform.Translate(moveX, 0, 0);

        if (this.transform.position.x <= ShieldMan.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
