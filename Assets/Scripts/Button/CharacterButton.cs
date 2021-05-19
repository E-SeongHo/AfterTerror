using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CharacterButton은 Space : 방패 올리는 동작 수행
// 누르고 있으면 방패 올려진 상태 유지 (최대 유지 시간 존재)

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private GameObject button_space; // 인스펙터에서 할당
    private GameObject target; // 버튼을 띄울 목표물 = Sheildman
    private bool buttonON = false;

    // Init
    private void Awake() 
    {
        target = GameObject.Find("Shieldman");    
        Debug.Log("CharacterButton : Target Found" + target);    
    }

    // buttonON 변수의 값에 따른다.
    private void Update()
    {
        if(!buttonON) // 버튼 안떠있으면
        {
            if(Input.GetMouseButtonDown(0)) // buttonmanager에서 OR Gamemanager에서 거리 판단함수
            {
                GiveButton(target);
                buttonON = true;
            }
        }
        else // 버튼 떠있으면
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                DeleteButton();
                buttonON = false;
            }
        }
    }
    // Buttons 
    // 접근자 public 필요할 수도 있음
    private void GiveButton(GameObject target)
    {
        ShieldmanController shieldmanController = target.GetComponent<ShieldmanController>();
        shieldmanController.GenerateButton(button_space);
    }
    private void DeleteButton()
    {
        ShieldmanController shieldmanController = target.GetComponent<ShieldmanController>();
        shieldmanController.DeleteButton();
    }
    
}