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
            target_controller.ShowButtons(ShieldmanController.Instance.GetSight());
            Debug.Log(target + "show");
            buttonON = true;
        }

        if (buttonON)
        {
            Debug.Log("now target : " + target);
            Debug.Log("now index : " + target_controller.GetTopIndex());
            InputProcess(target_controller.GetTopIndex());
        }
        else
        {
            // Debug.Log("no button");
        }

    }
    public void SetButtonState(bool value) { buttonON = value; }
    private void InputProcess(int buttonIndex)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 0) StartCoroutine("DeleteButton");
            else
            {
                target_controller.StartCoroutine("PlayXSheet");
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 1) StartCoroutine("DeleteButton");
            else
            {
                target_controller.StartCoroutine("PlayXSheet");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 2) StartCoroutine("DeleteButton");
            else
            {
                target_controller.StartCoroutine("PlayXSheet");
            }
        }
    }

    IEnumerator DeleteButton()
    {
        buttonON = false;
        
        animator = target_controller.GetTopButton().GetComponent<Animator>();
        animator.SetTrigger("click");
        yield return new WaitForSeconds(0.2f);

        // after click animation
        target_controller.HitProcess();
        // 만약 위의 HitProcess에 의해서 죽었다면 buttonON은 Controller 내부의 함수에 의해 false가 된다.

        // 죽지 않았으면 (다음 버튼 이어서 눌러야 하면)
        if (!target_controller.core.GetDieState())
            buttonON = true;
    }
}
