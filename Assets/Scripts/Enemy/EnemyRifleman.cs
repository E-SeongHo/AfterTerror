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
    private Animator animator;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        controller.SetCurrentHealth(maxHealth);
        controller.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();

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
        if (controller.GetRunState() && !controller.GetDieState())
        {
            controller.StartCoroutine("RunAwayProcess");
        }
    }
    // Bullet�� ��� �� ������ å��, �� ���Ĵ� å�� X
    private void ShotBullet()
    {
        if (!controller.GetInteractionState()) return;

        animator.SetTrigger("fire");
        bullet = BulletPool.Instance.AllocateBullet();
        // OnEnable���� v�� ����ϴϱ� position�� ������ �Ŀ� SetActive(true)�ؾ� �Ѵ�.
        Vector2 pos = transform.position;
        pos = pos + Vector2.left * 150f + Vector2.down * 60f;
        bullet.transform.position = pos;

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
