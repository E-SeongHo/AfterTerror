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
            if(!controller.GetDieState())  // 달려오다 죽었으면 그만 달려
                Run();
        }

        // if runaway 
        if (controller.GetRunState() && !controller.GetDieState())
        {
            controller.StartCoroutine("RunAwayProcess");
            perform = true; // interaction 과 달리기 시작할 때 기준 달라서 runaway 판정 이후에 위의 조건문 안들어가게 하기 위함
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
        // 호출시점 기준 목표 위치 설정 : stdpos
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

