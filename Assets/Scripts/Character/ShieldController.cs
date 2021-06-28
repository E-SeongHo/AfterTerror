using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ִ� 1�� ���� ��Ÿ�� 1��
public class ShieldController : MonoBehaviour
{
    [SerializeField] private float durationTime = 1f;
    [SerializeField] private float coolTime = 1f;
    private bool shieldON = false;
    private bool locked = false;

    public bool GetShieldState() { return shieldON; }
    
    // Input�� ���� ó���� FixedUpdate�� �ƴ� Update���� ó���Ѵ�.
    private void Update()
    {
        InputProcess();
    }
    private void LiftShield()
    {
        // animator ó�� 
        shieldON = true;
        // gameObject.transform.Translate(0, 50f, 0);
    }
    private void PutDownShield()
    {
        // animator ó��
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
