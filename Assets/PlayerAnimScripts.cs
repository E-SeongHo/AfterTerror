using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScripts : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {// this means game has started
            anim.SetTrigger("isStart");
        }
        if (Input.GetKey(KeyCode.G))
        { // this means player is ready to guard
            anim.SetTrigger("guard_on");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        { // this means player is ready to attack
            anim.SetTrigger("attack_on");
        }

        // if(){
        //     anim.SetBool("isFire", false);
        // }
        // if문 안에 시간 넣어주세요

        if (Input.GetKey(KeyCode.Space))
        { // this means player is jumping
            anim.SetTrigger("jump");
        }

        if (Input.GetKey(KeyCode.Z))
        { // this means player dead
            anim.SetTrigger("Health_zero");
        }

    }
}
