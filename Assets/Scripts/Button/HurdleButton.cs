using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleButton : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] buttons = new GameObject[4]; // 0 : up, 1 : down, 2 : left, 3 : right 
    private GameObject target;
    private int randIdx;
    List<GameObject> existHurdles = new List<GameObject>();
    HurdleController hurdleController;
    void Update() 
    {
        if(FindHurdles()) // 맵에 새로운 Hurdle이 있으면 리스트에 추가
        {
            GiveButtonToNewHurdle(); // 그 Hurdle에 button 생성
        }

        target = FindFunction.Instance.FindNearestObject(existHurdles);
        hurdleController = target.GetComponent<HurdleController>();

        if(hurdleController.buttonActive)
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
                if(!Hurdle.GetComponent<HurdleController>().buttonActive)
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
            if(!hurdleController.buttonActive)
            {
                hurdleController.GenerateButton(buttons[rand],rand);
            }
        }
    }
    private void InputProcess()
    {
        bool success = false;
        int key = hurdleController.buttonKind;
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
