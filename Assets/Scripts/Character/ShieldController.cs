using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִ� 1�� ���� ��Ÿ�� 1��
public class ShieldController : MonoBehaviour
{
    private float durationTime = 1f;
    private float coolTime = 0f;
    private Animator animator;
    private bool shieldON = false;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public bool GetShieldState() { return shieldON; }
    
    private void Update()
    {
        TimeCheck();
        InputProcess();
    }
    private void LiftShield()
    {
        animator.SetTrigger("guard");
        shieldON = true;
    }
    private void PutDownShield()
    {
        animator.SetBool("guard_end", true);
        shieldON = false;
    }
    public void ReSetShield()
    {
        Debug.Log("shield reset");
        PutDownShield();
        coolTime = 0f;
        durationTime = 1f;
    }
    private void TimeCheck()
    {
        // ��Ÿ��, ���ӽð� ó��
        if (shieldON)
        {
            durationTime -= Time.deltaTime;
            if (durationTime <= 0)
            {
                PutDownShield();
                coolTime = 1f;
            }
        }
        else // !shieldON
        {
            coolTime -= Time.deltaTime;
        }
    }
    private void InputProcess()
    {
        // Ű�Է� ó��
        if (coolTime <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            LiftShield();
            durationTime = 1f;
            coolTime = 1f;
        }
    }

    /*    private void InputProcess()
        {
            // Ű�Է� ó��
            if (coolTime <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    LiftShield();
                    durationTime = 1f;
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    if (durationTime < 0 && shieldON)
                    {
                        PutDownShield();
                        coolTime = 1f;
                    }
                }
                if (Input.GetKeyUp(KeyCode.Space) && shieldON)
                {
                    PutDownShield();
                    coolTime = 1f;
                }
            }
        }*/

}