using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// 1. �ڽ��� Enemy�� ���� x��ǥ�� ���� �� ���� ������ �̵�
//  -> x��ǥ�� ���� �۾����� 1.5ĳ���� �Ÿ���ŭ �̵� �� ���� �ȱ�

// 2. �ڽ� �Ҽ� �� �� ���� ����� �� �� 1.5ĳ���� �Ÿ���ŭ�� ����

// 3. ���� ����(������ �Ҽ� ���� ����Ϳ� ���� ��) ���� ����� �� �� 1.5ĳ���� �Ÿ�
//     �� ��ġ ���� �� �� ��ġ���� �״��� �޷���.
// !3��!

// ���� �Ȱ� ���� ü�� ���
public class EnemyShieldman : MonoBehaviour
{
    // Only shield
    [SerializeField] private int maxHealth = 4;
    [SerializeField] private int startHealth = 2;
    private float speed;

    private float initstd = 1920f;
    private bool perform = false;
    private bool set = false;

    private float viewSpeed;
    private EnemyController controller;
    private Vector2 stdpos;
    private List<GameObject> enemies;

    private Animator animator;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        controller.SetCurrentHealth(startHealth);
        controller.SetMaxHealth(maxHealth);
        viewSpeed = gameObject.transform.parent.GetComponent<FrontViewMove>().GetFrontViewSpeed();
        speed = viewSpeed * 2;
        animator = gameObject.GetComponent<Animator>();

        enemies = FindFunction.Instance.GetEnemiesInSameBlock(gameObject.transform.parent);
    }
    private void FixedUpdate()
    {
        // when parent(map block) showed in view
        if (gameObject.transform.parent.transform.position.x < initstd
            && !perform)
        {
            if (!set)
            {
                SetStandard();
            }
            if(!controller.GetDieState())  // �޷����� �׾����� �׸� �޷�
                Run();
        }

        // if runaway 
        if (controller.GetRunState() && !controller.GetDieState())
        {
            controller.StartCoroutine("RunAwayProcess");
            perform = true; // interaction �� �޸��� ������ �� ���� �޶� runaway ���� ���Ŀ� ���� ���ǹ� �ȵ��� �ϱ� ����
        }

        if (controller.GetDieState())
        {
            DieAction();
        }
    }
    private void SmashDownShield()
    {
        // shield animation
        animator.SetBool("arrive", true);
        // apply shield's health (+7)  
        controller.ChangeHealth(7);
        perform = true;
    }

    private void Run()
    {
        stdpos.x -= viewSpeed * Time.deltaTime;
        if (gameObject.transform.position.x - stdpos.x > -250f)
        {
            Vector2 pos = new Vector2(gameObject.transform.position.x - viewSpeed * Time.deltaTime * 2 , gameObject.transform.position.y);
            gameObject.transform.position = pos;
        }
        else
        {
            SmashDownShield();
        }
    }
    private void SetStandard()
    {
        // ȣ����� ���� ��ǥ ��ġ ���� : stdpos
        GameObject nearest = FindFunction.Instance.FindNearestObject(enemies);

        stdpos = nearest.transform.position;
        
        set = true;
    }
    private void DieAction()
    {
        animator.SetBool("die", true);
        // Destroy after 3 seconds
        Destroy(gameObject, 3f);
    }
}

