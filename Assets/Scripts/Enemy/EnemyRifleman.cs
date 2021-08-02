using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRifleman : MonoBehaviour
{ 
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int shotbulletbound = 3;

    // shot ����
    private GameObject bullet = null;
    private float autoShotTime = 4f;
    private float count;
    
    private EnemyController controller;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        controller.SetCurrentHealth(maxHealth);
        controller.SetMaxHealth(maxHealth);

        // initial fire time is random
        count = Random.Range(0.5f, 1.5f);
    }
    private void FixedUpdate()
    {
        if (controller.GetInteractionState())
        {
            AutoShotProcess();
            HitShotProcess();
        }
        if (controller.GetRunState())
        {
            Destroy(gameObject);
        }
    }
    // Bullet�� ��� �� ������ å��, �� ���Ĵ� å�� X
    private void ShotBullet()
    {
        bullet = BulletPool.Instance.AllocateBullet();
        // OnEnable���� v�� ����ϴϱ� position�� ������ �Ŀ� SetActive(true)�ؾ� �Ѵ�.
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }
    private void AutoShotProcess()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            ShotBullet();
            // after first shot, next fire is after 4f(autoShotTime) time
            count = autoShotTime;
        }
    }
    private void HitShotProcess()
    {
        if (controller.GetAttackCount() >= shotbulletbound)
        {
            ShotBullet();
            controller.ResetAttackCount();
        }
    }
}
