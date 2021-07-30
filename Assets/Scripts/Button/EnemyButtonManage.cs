using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyButtonManage Object�� �ʿ� Enemy�� ���� ����� ���� ã�� ��ư �ο�
// EnemyButton�� �� 9��
// EnemyButton�� ������ ������ EnemyButtonMange��ü�� ��� ���
// �ϳ����� ��ȣ�ۿ��ϴϱ� ����
public class EnemyButtonManage : MonoBehaviour
{
    // red ASD, green ASD, blue ASD

    // singleton
    public static EnemyButtonManage Instance;

    [SerializeField] private GameObject[] buttons = new GameObject[9];

    private GameObject[] enemies;
    private GameObject target = null;
    private Vector2 target_localpos;

    private GameObject nowButton;
    private bool buttonON = false;
    private int buttonidx; // A : 0, S : 1, D : 2

    private EnemyController targetController;

    // Getters
    public Vector2 GetTargetLocalPos() { return target_localpos; }
    public Vector2 GetTargetWorldPos() { return transform.position; }

    private void Awake()
    {
        Instance = this;
    }
    private void FixedUpdate()
    {
        // Find Nearest Enemy & Set variables with that Enemy
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        // (����ȭ)target ���� ��ü�� interactive �߿��� �ϱ�
        target = FindFunction.Instance.FindNearestObjectArrWithX(enemies);
        target_localpos = target.transform.position; 

        // target �ٲ𶧸� .. ȣ���ϰ� �ٲ㺸�� �������̿� �Ű����� 2���ֱ�?
        if(target!= null)
            targetController = target.GetComponent<EnemyController>();
        if (!buttonON)
        {
            if (target != null && targetController.GetInteractionState())
            {
                // enemyController = target.GetComponent<EnemyController>();
                GiveButton(target);
                Debug.Log("enemybutton gen : " + buttonidx);
            }
        }
        else
        {
            if (targetController.GetRunState())
            {
                buttonON = false;
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
        buttonON = true;
    }
    private void DeleteButton()
    {
        // Enemy�� ü�� ����
        targetController.ChangeHealth(-1);
        buttonON = false;
        nowButton = null;
        targetController.DeleteButton();

        // ���� �ʿ� ���� �� 
        // Update()���� ����ؼ� target�� �ٲ㼳���ϴϱ�
        if (targetController.GetCurrentHealth() <= 0)
        {

        }
    }
    // ������ �����ϸ� attackCount ����, �����ϸ� DeleteButton
    private void InputProcess(int buttonIndex)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (buttonIndex == 0) DeleteButton();
            else targetController.ChangeAttackCount(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (buttonIndex == 1) DeleteButton();
            else targetController.ChangeAttackCount(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (buttonIndex == 2) DeleteButton();
            else targetController.ChangeAttackCount(1);
        }
    }
    

}
