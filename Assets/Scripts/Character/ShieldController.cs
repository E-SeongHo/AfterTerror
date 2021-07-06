using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִ� 1�� ���� ��Ÿ�� 1��
public class ShieldController : MonoBehaviour
{
    [SerializeField] private float durationTime = 1f;
    [SerializeField] private float coolTime = 0f;
    private bool shieldON = false;

    public bool GetShieldState() { return shieldON; }
    
    // Input�� ���� ó���� FixedUpdate�� �ƴ� Update���� ó���Ѵ�.
    private void Update()
    {
        TimeCheck();
        InputProcess();
    }
    private void LiftShield()
    {
        // animator ó�� 
        gameObject.transform.Translate(0, 50f, 0);
        shieldON = true;
    }
    private void PutDownShield()
    {
        // animator ó��
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
        // ��Ÿ��, ���ӽð� ó��
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
    }
}