using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// 1. 자신이 Enemy중 가장 x좌표가 작을 때 까지 앞으로 이동
//  -> x좌표가 가장 작아지면 1.5캐릭터 거리만큼 이동 후 방패 꽂기

// 2. 자신 소속 블럭 중 가장 가까운 적 앞 1.5캐릭터 거리만큼에 방패

// 3. 현재 기준(지금은 소속 블럭이 모니터에 보일 때) 가장 가까운 적 앞 1.5캐릭터 거리
//     에 위치 설정 후 그 위치까지 죽더라도 달려감.
// !3번!

// 방패 꽂고 나면 체력 상승
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
                if (!SetStandard()) // 앞에 적이 없으면 바로 방패꽂음
                {
                    SmashDownShield();
                }
            }
            if(!controller.GetDieState())  // 달려오다 죽었으면 그만 달려
                Run();
        }

        // if runaway 
        if (controller.GetRunState() && !controller.GetDieState())
        {
            controller.StartCoroutine("RunAwayProcess");
            perform = true; // interaction 과 달리기 시작할 때 기준 달라서 runaway 판정 이후에 위의 조건문 안들어가게 하기 위함
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
        // 호출시점 기준 목표 위치 설정 : stdpos
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // interaction 아닌 적도 포함 ! 
        stdpos = FindFunction.Instance.FindNearestObjectArrWithX(enemies).transform.position;
        
        if (stdpos == null) success = false;
        else success = true;
        
        set = true;
        return success;
    }
}

