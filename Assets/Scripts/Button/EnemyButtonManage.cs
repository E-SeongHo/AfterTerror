using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

// EnemyButtonManage Object는 맵에 Enemy중 가장 가까운 적을 찾아 버튼 부여
// EnemyButton은 총 9개
// EnemyButton의 생성과 삭제는 EnemyButtonMange객체가 모두 담당
// 하나랑만 상호작용하니까 가능

/// <summary>
/// 버튼마다 Animator GetComponent 해야해서 무겁다
/// Button Pool 만들어서 배정하는식 
/// </summary>
public class EnemyButtonManage : MonoBehaviour
{
    // red ASD, green ASD, blue ASD

    // singleton
    public static EnemyButtonManage Instance;

    [SerializeField] private GameObject[] buttons = new GameObject[9];

    private GameObject[] enemies;
    private GameObject target_pre = null;
    private GameObject target = null;
    
    private GameObject nowButton;
    private Animator animator;

    private bool buttonON = false;
    private int buttonidx; // A : 0, S : 1, D : 2

    private EnemyController pre_targetController = null;
    private EnemyController targetController = null;

    // Getters
    public GameObject GetTarget() { return target; }
    // public Vector2 GetTargetLocalPos() { return target_localpos; }
    public Vector2 GetTargetWorldPos() { return transform.position; }
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        // Find Nearest Enemy & Set variables with that Enemy
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        target_pre = target;
        pre_targetController = targetController;

        target = FindFunction.Instance.FindNearestInteractionObjectArrWithX(enemies);

        // target이 바뀔때만 호출
        if (target != null && target_pre != target) 
            targetController = target.GetComponent<EnemyController>();
        
        if (!buttonON)
        {
            if (target != null && targetController.GetInteractionState())
            {
                GiveButton(target);
                Debug.Log("enemybutton gen : " + buttonidx);
            }
        }
        else
        {
            if (target_pre != target) // if target changed ( shieldman run )
            {
                buttonON = false;
                Debug.Log("target change");
                // delete previous target's button
                nowButton = null;
                pre_targetController.DeleteButton();
                // generate new target's button
                GiveButton(target);
                buttonON = true;
            }
            if (targetController.GetRunState())
            {
                buttonON = false;
                // knifeman 고려해야함
                targetController.SetInteractionState(false);

                Debug.Log(target + "run");
            }
            InputProcess(buttonidx);
        }
    }

    private void GiveButton(GameObject target)
    {
        // target 에게 button전달
        int randColor = Random.Range(0, 3);
        int randAlphabet = Random.Range(0, 3);
        // mapping 2D Array to 1D Array
        buttonidx = randAlphabet;
        nowButton = buttons[randAlphabet + randColor * 3];

        targetController.GenerateButton(nowButton);
        animator = targetController.myButton.GetComponent<Animator>();
               
        buttonON = true;
    }
    // 공격이 실패하면 attackCount 증가, 성공하면 DeleteButton
    private void InputProcess(int buttonIndex)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 0) StartCoroutine("ButtonClickAndDelete");
            else
            {
                targetController.StartCoroutine("PlayXSheet");
                targetController.ChangeAttackCount(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 1) StartCoroutine("ButtonClickAndDelete");
            else
            {
                targetController.StartCoroutine("PlayXSheet");
                targetController.ChangeAttackCount(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShieldmanController.Instance.StartCoroutine("AttackAnimation");
            if (buttonIndex == 2) StartCoroutine("ButtonClickAndDelete");
            else
            {
                targetController.StartCoroutine("PlayXSheet");
                targetController.ChangeAttackCount(1);
            }
        }
    }
    IEnumerator ButtonClickAndDelete()
    {
        animator.SetTrigger("click");
        Debug.Log("click");
        yield return new WaitForSeconds(0.2f);
        // after click animation
        targetController.ChangeHealth(-1 * ShieldmanController.Instance.GetAttackAbility());

        buttonON = false;
        nowButton = null;

        targetController.DeleteButton();
    }

    // 공격이 성공했을 때만 호출된다.
    private void DeleteButton()
    {
        // Enemy의 체력 감소
        targetController.ChangeHealth(-1 * ShieldmanController.Instance.GetAttackAbility());

        buttonON = false;
        nowButton = null;

        targetController.DeleteButton();

        // 아직 필요 없을 듯 
        // Update()에서 계속해서 target을 바꿔설정하니까
        if (targetController.GetCurrentHealth() <= 0)
        {
            nowButton = null;
        }
    }
}
