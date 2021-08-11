using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomberman : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int throwbombbound = 3;

    // throw dynamite... 
    [SerializeField] private GameObject dynamite_prefab;
    private float autoThrowTime = 4f;
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
            AutoThrowProcess();
            HitThrowProcess();
        }
        if (controller.GetRunState() && !controller.GetDieState())
        {
            controller.StartCoroutine("RunAwayProcess");
        }
        if (controller.GetDieState())
        {
            DieAction();
        }
    }
    private void ThrowDynamite()
    {
        animator.SetTrigger("throw");
        Vector3 init_pos = gameObject.transform.position + new Vector3(30f, 0, 0);
        GameObject newDynamite = Instantiate(dynamite_prefab, init_pos, Quaternion.identity);
    }
    private void AutoThrowProcess()
    {
        count -= Time.deltaTime;
        if(count < 0)
        {
            ThrowDynamite();
            // after first shot, next fire is after 4f(autoShotTime) time
            count = autoThrowTime;
        }
    }
    // 플레이어가 공격을 실패하면 카운트 후 공격
    private void HitThrowProcess()
    {
        if (controller.GetAttackCount() >= throwbombbound)
        {
            ThrowDynamite();
            controller.ResetAttackCount();
        }
    }
    private void DieAction()
    {
        animator.SetBool("die", true);
        // Destroy after 3 seconds
        Destroy(gameObject, 3f);
    }
    
}
