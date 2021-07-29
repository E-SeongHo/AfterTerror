using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimScripts : MonoBehaviour
{
    public float LimitTime;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetFloat("expireTime", 7f);
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.Q))
        {// this means game has started
            anim.SetBool("isStart", true);
        }
        if (Input.GetKey(KeyCode.G))
        { // this means player is ready to guard
            anim.SetTrigger("guard_on");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        { // this means player is ready to attack
            anim.SetBool("isAttack", true);
            anim.SetTrigger("attack_on");
            anim.SetTrigger("isFire");
            anim.SetFloat("expireTime", 7f);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetTrigger("specialAbility");
            anim.SetFloat("expireTime", 5f);
        }

        LimitTime = anim.GetFloat("expireTime");
        LimitTime -= Time.deltaTime;
        anim.SetFloat("expireTime", LimitTime);

        if (LimitTime <= 2)
        {
            anim.SetBool("isAttack", false);
        }


        if (Input.GetKey(KeyCode.Space))
        { // this means player is jumping
            anim.SetTrigger("jump");
        }


        if (Input.GetKey(KeyCode.Z))
        { // this means player dead
            anim.SetTrigger("Health_zero");
            anim.SetBool("isStart", false);
        }
    }
}
