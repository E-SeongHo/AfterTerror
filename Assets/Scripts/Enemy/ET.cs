using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ET : MonoBehaviour
{
    // info
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int throw_bound = 3;
    [SerializeField] private float downhill_spd = 100f;
    [SerializeField] private float run_spd;
    [SerializeField] private int slash_damage = 2;

    private GameObject player;
    private float attack_point = 200f;
    private float landing_point = -228f;

    // flag
    private bool attack = false; // 공격 했는지 여부 flag
    private bool anim_play = false; // 공중에서 죽을 떄 flag
    private bool die_play = false;


    // throw kinfe...
    [SerializeField] private GameObject throwing_knife; // 투척용 나이프 prefab
    private bool flying = true;
    private float autoThrowTime = 4f;
    private float count;

    // controllers
    private EnemyController controller;
    private ShieldController shield;
    private Animator animator;
    private Animator parachute_anim;

    private void Start()
    {
        controller = gameObject.GetComponent<EnemyController>();
        controller.SetCurrentHealth(maxHealth);
        controller.SetMaxHealth(maxHealth);
        player = GameObject.FindGameObjectWithTag("Player");
        shield = player.transform.gameObject.GetComponent<ShieldController>();
        run_spd = gameObject.transform.parent.GetComponent<FrontViewMove>().GetFrontViewSpeed() * 3f;

        animator = gameObject.GetComponent<Animator>();
        parachute_anim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // 플레이어 앞에서 공격 할 때 버튼 안뜨게 하려면 여기 interaciton 손보면됨
        if (controller.GetInteractionState())
        {
            if (flying)
            {
                FlyingAction();
                AutoThrowProcess();
                HitThrowProcess();
            }
            // after landing
            else
            {
                RushToPlayer();
            }
        }
        if (controller.GetDieState() && !die_play)
        {
            DieAction();
        }

    }
    private void AutoThrowProcess()
    {
        count -= Time.deltaTime;
        if (count < 0)
        {
            ThrowKnife();
            // after first shot, next fire is after 4f(autoShotTime) time
            count = autoThrowTime;
        }
    }
    private void HitThrowProcess()
    {
        if (controller.GetAttackCount() >= throw_bound)
        {
            ThrowKnife();
            controller.ResetAttackCount();
        }
    }
    private void ThrowKnife()
    {
        animator.SetTrigger("thorw");
        Vector3 init_pos = gameObject.transform.position + new Vector3(-30f, 0, 0);
        GameObject newKnife = Instantiate(throwing_knife, init_pos, Quaternion.identity);
    }
    private void SlashKnife()
    {
        animator.SetBool("arrive", true);
        int amount;
        if (shield.GetShieldState())
        {
            amount = 0;
            Debug.Log("Shield");
            shield.ReSetShield();
        }
        else
        {
            amount = slash_damage * -1;
            Debug.Log("Non Shield" + "damage : " + amount);
            ShieldmanController.Instance.ChangeHealth(amount);
        }

        attack = true;
    }
    private void FlyingAction()
    {
        Debug.Log(transform.position.y);
        if (gameObject.transform.position.y > landing_point)
        {
            // downhill
            gameObject.transform.Translate(Vector2.down * downhill_spd * 5 * Time.deltaTime);
        }
        else
        {
            // landing action
            animator.SetBool("land", true);
            parachute_anim.SetBool("land", true);
            flying = false;
        }
    }
    private void RushToPlayer()
    {
        gameObject.transform.Translate(Vector2.left * run_spd * Time.deltaTime);

        if (gameObject.transform.position.x - player.transform.position.x < attack_point && !attack)
        {
            // 맞는거 판정 잘...
            SlashKnife();
            Destroy(gameObject, 3f);
        }
    }
    private void DieAction()
    {
        if (flying)
        {
            if (!anim_play)
            {
                parachute_anim.SetBool("die", true);
                Destroy(gameObject.transform.GetChild(0).gameObject, 1f);
                animator.SetBool("skydie", true);
                anim_play = true;
            }
            gameObject.transform.Translate(Vector2.down * downhill_spd * 10 * Time.deltaTime);
            if (gameObject.transform.position.y < landing_point)
            {
                animator.SetBool("die", true);
                Destroy(gameObject, 3f);
                die_play = true;
            }
        }
        else
        {
            animator.SetBool("die", true);
            Destroy(gameObject, 3f);
            die_play = true;
        }
    }
}
