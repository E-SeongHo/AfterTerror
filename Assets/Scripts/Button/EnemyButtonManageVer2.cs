using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonManageVer2 : MonoBehaviour
{
    public static EnemyButtonManageVer2 Instance;

    private GameObject pretarget = null;
    private GameObject target = null;

    private GameObject nowbutton;
    private bool buttonON = false;

    private Animator animator;

    private EnemyButtonInfo pretarget_controller = null;
    private EnemyButtonInfo target_controller = null;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        pretarget = target;

        if (EnemyPool.Instance.GetPoolSize() > 0)
            target = EnemyPool.Instance.GetNearestEnemy();

        // when target changed
        if (target != null && target != pretarget)
        {
            if(buttonON) // means enemyshieldman passed target 
            {
                target_controller.EmptyOutShowingQueue();
            }
            target_controller = target.GetComponent<EnemyButtonInfo>();

            // show button
            if (target_controller.special_ON)
                target_controller.ShowSpecialButton();
            else
                target_controller.ShowButtons(ShieldmanController.Instance.GetSight());
            Debug.Log(target + "show");
            buttonON = true;
        }

        if (buttonON)
        {
            //Debug.Log("now target : " + target);
            if (target_controller.special_ON)
            {
                SpecialButton now = target_controller.GetPresentSpecial();
                int idx = now.index;
                int type = now.type;

                switch(type)
                {
                    case 0:
                        Type0InputProcess(idx);
                        break;
                    case 1:
                        Type1InputProcess(idx);
                        break;
                }
            }            
            else
            {
                //Debug.Log("now index : " + target_controller.GetTopIndex());
                InputProcess(target_controller.GetTopIndex());
            }
        }
        else
        {
            // Debug.Log("no button");
        }

    }
    public void SetButtonState(bool value) { buttonON = value; }
    private void InputProcess(int button_index)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (button_index == 0) StartCoroutine("DeleteButton");
            else
            {
                target_controller.StartCoroutine("PlayXSheet");
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (button_index == 1) StartCoroutine("DeleteButton");
            else
            {
                target_controller.StartCoroutine("PlayXSheet");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (button_index == 2) StartCoroutine("DeleteButton");
            else
            {
                target_controller.StartCoroutine("PlayXSheet");
            }
        }
    }
    private void Type0InputProcess(int button_index)
    {
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (button_index == 0) DeleteGauge();
            else
                target_controller.StartCoroutine("PlayXSheet");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (button_index == 1) DeleteGauge();
            else
                target_controller.StartCoroutine("PlayXSheet");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (button_index == 2) DeleteGauge();
            else
                target_controller.StartCoroutine("PlayXSheet");
        }
    }
    private void Type1InputProcess(int button_index)
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("a");
            if (button_index == 0) FillGauge();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("s");
            if (button_index == 1) FillGauge();
        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("d");
            if (button_index == 2) FillGauge();
        }
    }

    IEnumerator DeleteButton()
    {
        buttonON = false; // click animation 진행 중 INPUT프로세스 진행 안되게 flag
        
        animator = target_controller.GetTopButton().GetComponent<Animator>();
        animator.SetTrigger("click");
        yield return new WaitForSeconds(0.2f);

        // after click animation
        target_controller.HitProcessBasic();
        // 만약 위의 HitProcess에 의해서 죽었다면 buttonON은 Controller 내부의 함수에 의해 false가 된다.

        // 죽지 않았으면 (다음 버튼 이어서 눌러야 하면)
        if (!target_controller.core.GetDieState())
            buttonON = true;
    }
    // Delete for type0 (연타버튼)
    private void DeleteGauge()
    {
        target_controller.HitProcessType0();
    }
    // Filling for type1 (꾹 누르기 버튼)
    private void FillGauge()
    {
        target_controller.HitProcessType1();
    }
}
