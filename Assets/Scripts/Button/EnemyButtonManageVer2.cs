using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyButtonManageVer2 : MonoBehaviour
{
    public static EnemyButtonManageVer2 Instance;

    private int player_sight; // the number of buttons that player can see.. it can be increase

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
        pretarget_controller = target_controller;

        if (EnemyPool.Instance.GetPoolSize() > 0)
            target = EnemyPool.Instance.GetNearestEnemy();

        // run only when target changed
        if (target != null && target != pretarget)
        {
            target_controller = target.GetComponent<EnemyButtonInfo>();
            target_controller.ShowButtons(ShieldmanController.Instance.GetSight());
            Debug.Log("show");
            buttonON = true;
        }
        
        if (buttonON)
        {
            InputProcess(target_controller.GetTopIndex());
        }

    }
    private void InputProcess(int buttonIndex)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 0) target_controller.DeleteButton();
            else
            {
                target_controller.StartCoroutine("PlayXsheet");
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 1) target_controller.DeleteButton();
            else
            {
                target_controller.StartCoroutine("PlayXsheet");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 2) target_controller.DeleteButton();
            else
            {
                target_controller.StartCoroutine("PlayXsheet");
            }
        }
    }
}
