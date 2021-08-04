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
    private GameObject[] enemies;

    private Animator animator;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        controller.SetCurrentHealth(startHealth);
        controller.SetMaxHealth(maxHealth);
        viewSpeed = gameObject.transform.parent.GetComponent<FrontViewMove>().GetFrontViewSpeed();
        speed = viewSpeed * 3;
        animator = gameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        // when parent(map block) showed in view
        if (gameObject.transform.parent.transform.position.x < initstd
            && !perform)
        {
            if (!set)
            {
                if (!SetStandard()) // �տ� ���� ������ �ٷ� ���в���
                {
                    SmashDownShield();
                }
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
    }
    private void SmashDownShield()
    {
        // shield animation
        animator.SetBool("arrive", true);
        // apply shield's health (+7)  
        controller.ChangeHealth(7);
        perform = true;
    }
 /*   IEnumerator RunForward()
    {
        *//*// run animation 
        Debug.Log(gameObject.transform.parent);
        Debug.Log(gameObject.transform.position.x - EnemyButtonManage.Instance.GetTargetWorldPos().x);
        while (EnemyButtonManage.Instance.GetTarget() == null)
            ;
        while (gameObject.transform.position.x - EnemyButtonManage.Instance.GetTargetWorldPos().x > -250f)
        {
            // Vector2 target_pos = EnemyButtonManage.Instance.GetTargetLocalPos();

            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }

        SmashDownShield();
        perform = true;

        yield return null;*//*

        if (!set)
            SetStandard();
        while (gameObject.transform.position.x - stdpos.x > -250f)
        {
            Debug.Log(gameObject.transform.position.x - stdpos.x);
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }
        SmashDownShield();

        yield return null;
    }*/
    private void Run()
    {
        Debug.Log("run");
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

    private bool SetStandard()
    {
        bool success = false;
        // ȣ����� ���� ��ǥ ��ġ ���� : stdpos
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // interaction �ƴ� ���� ���� ! 
        stdpos = FindFunction.Instance.FindNearestObjectArrWithX(enemies).transform.position;
        
        if (stdpos == null) success = false;
        else success = true;
        
        set = true;
        return success;
    }
}

