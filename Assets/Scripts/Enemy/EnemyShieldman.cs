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

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        controller.SetCurrentHealth(startHealth);
        controller.SetMaxHealth(maxHealth);
        viewSpeed = gameObject.transform.parent.GetComponent<FrontViewMove>().GetFrontViewSpeed();
        speed = viewSpeed * 3;
    }
    private void FixedUpdate()
    {
        // when parent(map block) showed in view
        if (gameObject.transform.parent.transform.position.x < initstd
            && !perform)
        {
            if (!set)
                SetStandard();
            Run();
        }

        // if runaway 
        if (controller.GetRunState())
        {
            Destroy(gameObject);
        }
    }
    private void SmashDownShield()
    {
        // shield animation
        gameObject.transform.Translate(0, 50f, 0, Space.World);
        // apply shield's health (+7)  
        controller.ChangeHealth(7);
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
        stdpos.x -= viewSpeed * Time.deltaTime;
        if (gameObject.transform.position.x - stdpos.x > -250f)
        {
            Vector2 pos = new Vector2(gameObject.transform.position.x - viewSpeed * Time.deltaTime * 2 , gameObject.transform.position.y);
            gameObject.transform.position = pos;
        }
        else
        {
            SmashDownShield();
            perform = true;
        }
    }

    private void SetStandard()
    {
        // ȣ����� ���� ��ǥ ��ġ ���� : stdpos
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        stdpos = FindFunction.Instance.FindNearestObjectArrWithX(enemies).transform.position;
        set = true;
    }
}

