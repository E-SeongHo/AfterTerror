using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public GameObject button_space;
    GameObject target;
    bool buttonON = false;
    private void Awake() 
    {
        target = GameObject.Find("Shieldman");    
        Debug.Log(target);    
    }
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
    void GiveButton(GameObject target)
    {
        ShieldmanController shieldmanController = target.GetComponent<ShieldmanController>();
        shieldmanController.GenerateButton(button_space);
    }
    void DeleteButton()
    {
        ShieldmanController shieldmanController = target.GetComponent<ShieldmanController>();
        shieldmanController.DeleteButton();
    }
    
}