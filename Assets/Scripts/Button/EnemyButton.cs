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
    [SerializeField] private GameObject[][] buttons; // 3x3 배열
    
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
        // target (nearest Enemy)에게 button전달
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
        // buttonIdx에 맞는 값 들어오면 DeleteButton 호출
        if(Input.GetKeyDown(KeyCode.A)
        {
            
        }
    }

}
