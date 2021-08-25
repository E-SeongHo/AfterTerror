using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScripts : MonoBehaviour
{
    [SerializeField] GameObject mine;
    void Start()
    {
        mine.SetActive(false);
    }

    void Update()
    {
        if (mine.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameObject arrow = mine.GetComponentInChildren<GameObject>();
                arrow.SetActive(false);
                Debug.Log("arrowKey pressed");
            }
        }
    }
}
