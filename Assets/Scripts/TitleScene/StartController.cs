using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    bool isTutored = false;
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void OnMouseEnter()
    {
        animator.SetBool("mouseON", true);
    }
    private void OnMouseExit()
    {
        animator.SetBool("mouseON", false);
    }
    private void OnMouseDown()
    {
        // game start !!!
        if (!isTutored)
        {
            SceneLoader.Instance.LoadScene("Test");
            isTutored = true;
        }
        else
            SceneLoader.Instance.LoadScene("Stage1");
    }
}
