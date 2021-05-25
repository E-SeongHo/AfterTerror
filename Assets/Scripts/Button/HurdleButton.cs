using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Hurdle에 생성되는 버튼은 UP, DOWN, LEFT, RIGHT
// HurdleButtonManage Object는 맵에 존재하는 Hurdle을 List로 보관
// Hurdle중 한 개 Hurdle에 대해 상호작용 (가장 가까운 Hurdle) - Input 감지
public class HurdleButton : MonoBehaviour
{
    // 0 : up, 1 : down, 2 : left, 3 : right 
    [SerializeField] private GameObject[] buttons = new GameObject[4]; 
    private GameObject target;
    private int randIdx; // 랜덤하게 각 Hurdle에 버튼 생성

    private List<GameObject> existHurdles = new List<GameObject>(); // 맵에 존재하는 Hurdle 관리
    private HurdleController hurdleController;
    void Update() 
    {
        // 프레임마다 호출될 필요 X -> FixedUpdate   
        if(FindHurdles()) // 맵에 새로운 Hurdle이 있으면 리스트에 추가
        {
            GiveButtonToNewHurdle(); // 그 Hurdle에 button 생성
        }

        target = FindFunction.Instance.FindNearestObject(existHurdles);
        if (target != null)
        {
            hurdleController = target.GetComponent<HurdleController>();
        }
        else return;

        if(hurdleController.GetButtonON())
        {
            InputProcess();
        }
    }
    private bool FindHurdles() // 연산 효율 생각해볼것
    {
        bool addNew = false;
        GameObject[] Hurdles;
        Hurdles = GameObject.FindGameObjectsWithTag("Hurdle");
        if (Hurdles == null) return false;
        else 
        {
            foreach(GameObject Hurdle in Hurdles)
            {
                if(!Hurdle.GetComponent<HurdleController>().GetButtonON())
                {
                    existHurdles.Add(Hurdle);
                    addNew = true;    
                }   
            }
        }
        return addNew; // 새로운 Hurdle이 추가됐으면 true반환
    }
    private void GiveButtonToNewHurdle()
    {
        int rand;
        foreach(GameObject Hurdle in existHurdles)
        {
            hurdleController = Hurdle.GetComponent<HurdleController>();
            rand = Random.Range(0,4);
            if(!hurdleController.GetButtonON())
            {
                hurdleController.GenerateButton(buttons[rand],rand);
            }
        }
    }
    private void InputProcess()
    {
        bool success = false;
        int key = hurdleController.GetButtonIdx();
        switch(key)
        {
            case 0: // Up key
                if(Input.GetKeyDown(KeyCode.UpArrow)) success = true;
                break;
            case 1: // Down key
                if(Input.GetKeyDown(KeyCode.DownArrow)) success = true;
                break;
            case 2: // Left key
                if(Input.GetKeyDown(KeyCode.LeftArrow)) success = true;
                break;
            case 3: // Right key
                if(Input.GetKeyDown(KeyCode.RightArrow)) success = true;
                break;
        }
        if(success) 
        {

            DeleteButton();
            existHurdles.Remove(target);
        }
    }
    private void DeleteButton()
    {
        hurdleController.DeleteButton();
    }
}
