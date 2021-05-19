using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyButtonManage Object�� �ʿ� Enemy�� ���� ����� ���� ã�� ��ư �ο�
// EnemyButton�� �� 9��
// EnemyButton�� ������ ������ EnemyButtonMange��ü�� ��� ���
// �ϳ����� ��ȣ�ۿ��ϴϱ� ����
public class EnemyButton : MonoBehaviour
{   
    // red ASD, green ASD, blue ASD
    [SerializeField] private GameObject[][] buttons; // 3x3 �迭
    
    private GameObject[] enemies;
    private GameObject target = null;

    private GameObject nowButton;
    private bool buttonON = false;
    private int buttonidx; // A : 0, S : 1, D : 2
    
    private EnemyController enemyController;

    private void Update()
    {
        if(!buttonON)
        {
            // Find Nearest Enemy
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            target = FindFunction.Instance.FindNearestObjectArr(enemies);
            enemyController = target.GetComponent<EnemyController>();
            GiveButton(target);
        }
        else
        {
            InputProcess();
        }
    }

    private void GiveButton(GameObject target)
    {
        // target (nearest Enemy)���� button����
        int randColor = Random.Range(0,3);
        int randAlphabet = Random.Range(0,3);
        buttonidx = randAlphabet;
        nowButton = buttons[randColor][randAlphabet];
        enemyController.GenerateButton(nowButton);
    }
    private void DeleteButton()
    {
        
    }
    private void InputProcess()
    {
        // buttonIdx�� �´� �� ������ DeleteButton ȣ��
        if(Input.GetKeyDown(KeyCode.A)
        {
            
        }
    }

}
