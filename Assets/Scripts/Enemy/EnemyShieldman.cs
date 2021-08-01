using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1. �ڽ��� Enemy�� ���� x��ǥ�� ���� �� ���� ������ �̵�
//  -> x��ǥ�� ���� �۾����� 1.5ĳ���� �Ÿ���ŭ �̵� �� ���� �ȱ�

// 2. �ڽ� �Ҽ� �� �� ���� ����� �� �� 1.5ĳ���� �Ÿ���ŭ�� ����

// 3. ���� ����(������ �Ҽ� ���� ����Ϳ� ���� ��) ���� ����� �� �� 1.5ĳ���� �Ÿ�
//     �� ��ġ ���� �� �� ��ġ���� �״��� �޷���.
// 3������...

// ���� �Ȱ� ���� ü�� ���
public class EnemyShieldman : MonoBehaviour
{
    // Only shield
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int startHealth = 3;
    [SerializeField] private float speed = 5f;

    private bool perform = false;
    private bool set = false;
    private EnemyController controller;
    private GameObject standard;
    private GameObject[] enemies;

    private void Start()
    {
        controller = GetComponent<EnemyController>();
        controller.SetCurrentHealth(startHealth);
        controller.SetMaxHealth(maxHealth);
    }
    private void FixedUpdate()
    {
        if (gameObject.transform.parent.transform.position.x < 1920
            && !perform)
        {
            StartCoroutine("RunForward");
        }

        if (controller.GetRunState())
        {
            Destroy(gameObject);
        }
    }
    private void SmashDownShield()
    {
        // shield animation
        if (!perform)
        {
            gameObject.transform.Translate(0, 50f, 0, Space.World);
            // apply shield's health (+7)  
            // controller.ChangeHealth(7);
            perform = true;
        }
    }
    IEnumerator RunForward()
    {
        /*// run animation 
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

        yield return null;*/

        if(!set) 
            SetStandard();
        Vector2 stdpos = standard.transform.position;
        while (gameObject.transform.position.x - stdpos.x > -250f)
        {
            Debug.Log(gameObject.transform.position.x - stdpos.x);
            gameObject.transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
            yield return new WaitForFixedUpdate();
        }
        SmashDownShield();

        yield return null;
    }
    private void SetStandard()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        standard = FindFunction.Instance.FindNearestObjectArrWithX(enemies);
        set = true;
    }
}

