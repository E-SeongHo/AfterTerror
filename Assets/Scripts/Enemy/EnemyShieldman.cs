using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1. 자신이 Enemy중 가장 x좌표가 작을 때 까지 앞으로 이동
//  -> x좌표가 가장 작아지면 1.5캐릭터 거리만큼 이동 후 방패 꽂기

// 2. 자신 소속 블럭 중 가장 가까운 적 앞 1.5캐릭터 거리만큼에 방패

// 3. 현재 기준(지금은 소속 블럭이 모니터에 보일 때) 가장 가까운 적 앞 1.5캐릭터 거리
//     에 위치 설정 후 그 위치까지 죽더라도 달려감.
// 3번으로...

// 방패 꽂고 나면 체력 상승
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

