using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionButton : MonoBehaviour
{
    PauseController pauseController;

    private void Start()
    {
        pauseController = GameObject.Find("GameManage").GetComponent<PauseController>();    
    }

    private void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(pauseController);
        pauseController.GenerateWindow();
    }
}
