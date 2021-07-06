using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 최대 1초 방어가능 쿨타임 1초
public class ShieldController : MonoBehaviour
{
    [SerializeField] private float durationTime = 1f;
    [SerializeField] private float coolTime = 0f;
    private bool shieldON = false;

    public bool GetShieldState() { return shieldON; }
    
    // Input에 대한 처리는 FixedUpdate가 아닌 Update에서 처리한다.
    private void Update()
    {
        TimeCheck();
        InputProcess();
    }
    private void LiftShield()
    {
        // animator 처리 
        gameObject.transform.Translate(0, 50f, 0);
        shieldON = true;
    }
    private void PutDownShield()
    {
        // animator 처리
        gameObject.transform.Translate(0, -50f, 0);
        shieldON = false;
    }
    public void ReSetShield()
    {
        PutDownShield();
        coolTime = 0f;
        durationTime = 1f;
    }
    private void TimeCheck()
    {
        // 쿨타임, 지속시간 처리
        if (shieldON)
        {
            durationTime -= Time.deltaTime;
        }
        else // !shieldON
        {
            coolTime -= Time.deltaTime;
        }
    }
    private void InputProcess()
    {
        // 키입력 처리
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
    }
}