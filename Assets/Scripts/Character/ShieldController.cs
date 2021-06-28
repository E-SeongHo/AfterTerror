using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 최대 1초 방어가능 쿨타임 1초
public class ShieldController : MonoBehaviour
{
    [SerializeField] private float durationTime = 1f;
    [SerializeField] private float coolTime = 1f;
    private bool shieldON = false;
    private bool locked = false;

    public bool GetShieldState() { return shieldON; }
    
    // Input에 대한 처리는 FixedUpdate가 아닌 Update에서 처리한다.
    private void Update()
    {
        InputProcess();
    }
    private void LiftShield()
    {
        // animator 처리 
        shieldON = true;
        // gameObject.transform.Translate(0, 50f, 0);
    }
    private void PutDownShield()
    {
        // animator 처리
        shieldON = false;
        //gameObject.transform.Translate(0, -50f, 0);
    }
    private void InputProcess()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LiftShield();
            durationTime = 1f;
            coolTime = 1f;
            locked = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            durationTime -= Time.deltaTime;
            if (durationTime < 0)
            {
                PutDownShield();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            coolTime -= Time.deltaTime;
            if (coolTime < 0)
            {
                locked = false;
            }
        }
    }
}
