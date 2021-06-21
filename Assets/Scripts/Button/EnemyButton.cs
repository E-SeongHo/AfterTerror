using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyButtonManage Object는 맵에 Enemy중 가장 가까운 적을 찾아 버튼 부여
// EnemyButton은 총 9개
// EnemyButton의 생성과 삭제는 EnemyButtonMange객체가 모두 담당
// 하나랑만 상호작용하니까 가능
public class EnemyButton : MonoBehaviour
{
    // red ASD, green ASD, blue ASD
    // 굳이 GameObject로 안하고 Sprite배열로 해도 될듯
    // 게임 무거워지면 고려
    [SerializeField] private GameObject[] buttons = new GameObject[9];

    private GameObject[] enemies;
    private GameObject target = null;

    private GameObject nowButton;
    private bool buttonON = false;
    private int buttonidx; // A : 0, S : 1, D : 2

    private EnemyController targetController;

    private void Update()
    {
        // Find Nearest Enemy & Set variables with that Enemy
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // (최적화)target 선정 자체를 interactive 중에서 하기
        target = FindFunction.Instance.FindNearestObjectArrWithX(enemies);
        // target 바뀔때만 .. 호출하게 바꿔보자 포인터이용 매개변수 2개주기?
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
        // target 에게 button전달
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
        // Enemy의 체력 감소
        targetController.ChangeHealth(-1);
        buttonON = false;
        nowButton = null;
        targetController.DeleteButton();

        // 아직 필요 없을 듯 
        // Update()에서 계속해서 target을 바꿔설정하니까
        if (targetController.GetCurrentHealth() <= 0)
        {

        }
    }
    // 공격이 실패하면 attackCount 증가, 성공하면 DeleteButton
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
