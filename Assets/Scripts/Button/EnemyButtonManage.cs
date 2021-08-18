using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

// EnemyButtonManage Object�� �ʿ� Enemy�� ���� ����� ���� ã�� ��ư �ο�
// EnemyButton�� �� 9��
// EnemyButton�� ������ ������ EnemyButtonMange��ü�� ��� ���
// �ϳ����� ��ȣ�ۿ��ϴϱ� ����

/// <summary>
/// ��ư���� Animator GetComponent �ؾ��ؼ� ���̴�
/// Button Pool ���� �����ϴ½� 
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

        // target�� �ٲ𶧸� ȣ��
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
                // knifeman ����ؾ���
                targetController.SetInteractionState(false);

                Debug.Log(target + "run");
            }
            InputProcess(buttonidx);
        }
    }

    private void GiveButton(GameObject target)
    {
        // target ���� button����
        int randColor = Random.Range(0, 3);
        int randAlphabet = Random.Range(0, 3);
        // mapping 2D Array to 1D Array
        buttonidx = randAlphabet;
        nowButton = buttons[randAlphabet + randColor * 3];

        targetController.GenerateButton(nowButton);
        animator = targetController.myButton.GetComponent<Animator>();
               
        buttonON = true;
    }
    // ������ �����ϸ� attackCount ����, �����ϸ� DeleteButton
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

    // ������ �������� ���� ȣ��ȴ�.
    private void DeleteButton()
    {
        // Enemy�� ü�� ����
        targetController.ChangeHealth(-1 * ShieldmanController.Instance.GetAttackAbility());

        buttonON = false;
        nowButton = null;

        targetController.DeleteButton();

        // ���� �ʿ� ���� �� 
        // Update()���� ����ؼ� target�� �ٲ㼳���ϴϱ�
        if (targetController.GetCurrentHealth() <= 0)
        {
            nowButton = null;
        }
    }
}
